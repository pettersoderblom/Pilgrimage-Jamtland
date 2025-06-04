import { blueFlag, yellowFlag, redFlag, greenFlag } from "./Markers.js";


export let trailsInformation = [
    { id: 4, name: 'Indalsleden', color: 'blue', trailIcon: blueFlag, startPoint: [62.4855311, 17.3244924], endPoint: [63.3160351, 12.1017855], displayName: 'Indalsleden' },
    { id: 2, name: 'Karboleleden', color: 'yellow', trailIcon: yellowFlag, startPoint: [60.565463, 17.4374], endPoint: [62.80033, 13.056407], displayName: 'Kårböleleden' },
    { id: 3, name: 'st-olovsleden', color: 'red', trailIcon: redFlag, startPoint: [62.386808961183199, 17.315430391312599], endPoint: [63.427208, 10.396927], displayName: 'S:t Olovsleden' }
];


export function addUserLocation(map) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                const userLat = position.coords.latitude;
                const userLng = position.coords.longitude;
                map.setView([userLat, userLng], 13);
                L.marker([userLat, userLng]).addTo(map).bindPopup('<b>Du är här!</b>').openPopup();
            },
            function () { alert('Kunde inte hämta din plats'); }
        );
    } else {
        alert('Din webbläsare stödjer ej plats hämtning');
    }
}

// Function to fetch & display GeoJSON routes
export async function fetchAndDisplayRoute(map) {
    try {
        const response = await fetch('/js/Maps/geojson/Jamt-norge-leden.geojson');
        if (!response.ok) throw new Error('Failed to load the GeoJSON file.');

        const geoJsonData = await response.json();
        if (!geoJsonData || !geoJsonData.features) throw new Error('GeoJSON file does not contain features.');

        // Extract and filter nodes
        const nodes = geoJsonData.features
            .filter(feature => feature.geometry.type === 'Point')
            .map(feature => ({
                id: feature.properties["@id"],
                coordinates: feature.geometry.coordinates
            }));

        const startCoordinates = [14.9847101, 62.864618];
        const endCoordinates = [12.0746285, 62.9024744];

        // Ensure route is ordered correctly
        const orderedCoordinates = orderNodesByProximity(nodes, startCoordinates, endCoordinates);
        if (orderedCoordinates.length < 2) throw new Error('Not enough coordinates to build a route.');

        // Create LineString feature and display it on the map
        L.geoJSON(
            { type: 'Feature', geometry: { type: 'LineString', coordinates: orderedCoordinates } },
            { style: { color: 'Green', weight: 4  },
            onEachFeature: function (feature, layer) {
                layer.on('click', function (event) {
                    L.popup()
                        .setLatLng(event.latlng)
                        .setContent(`
                        <b>Jämt Norge leden</b><br>
                        <a href="/Trails/TrailDetails?id=1" target="_blank">Se mer information</a>
                    `)
                        .openOn(map);
                  });
                }
            }
        ).addTo(map);

        // Add markers for start and end points
        L.marker([startCoordinates[1], startCoordinates[0]], { icon: greenFlag }).addTo(map).bindPopup('Start: Jämt-norge-leden');
        L.marker([endCoordinates[1], endCoordinates[0]], { icon: greenFlag }).addTo(map).bindPopup('Slut: Jämt-norge-leden');

    } catch (error) {
        alert('Could not load the route.');
        console.error('Error loading or displaying the route:', error);
    }
}

// Function to fetch & display GPX routes
export async function fetchAndDisplayGPXRoute(map, trailId, trailName, trailColor, startPoint, endPoint, trailIcon, trailDisplayName) {
    try {
        const gpxLayer = new L.GPX(`/js/Maps/Gpx/${trailName}.gpx`, {
            async: true,
            marker_options: {
                startIconUrl: '/images/MapImages/hide.png',  
                endIconUrl: '/images/MapImages/hide.png',      
                shadowUrl: '/images/MapImages/hide.png',         
                iconSize: [1, 1],   
                shadowSize: [1, 1]
            },
            polyline_options: {
                color: `${trailColor}`,
                weight: 4
            }
        });


        gpxLayer.addTo(map);
        gpxLayer.on('loaded', function (e) { map.fitBounds(e.target.getBounds()); });

        L.marker(startPoint, { icon: trailIcon }).addTo(map).bindPopup(`Start: ${trailName}`);
        L.marker(endPoint, { icon: trailIcon }).addTo(map).bindPopup(`Slut: ${trailName}`);

        // Make GPX route clickable
        gpxLayer.on('addline', function (e) {
            e.line.on('click', function (event) { 
                L.popup()
                    .setLatLng(event.latlng)
                    .setContent(`
                        <b>${trailDisplayName}</b><br>
                        <a href="/Trails/TrailDetails?id=${trailId}" target="_blank">Se mer information</a>

                    `)
                    .openOn(map);
            });
        });


    } catch (error) {
        console.error('Error loading or displaying the GPX route:', error);
        alert('Could not load the GPX route.');
    }
}

export function getDistance(coord1, coord2) {
    const [lon1, lat1] = coord1;
    const [lon2, lat2] = coord2;
    const R = 6371e3;

    const φ1 = lat1 * (Math.PI / 180);
    const φ2 = lat2 * (Math.PI / 180);
    const Δφ = (lat2 - lat1) * (Math.PI / 180);
    const Δλ = (lon2 - lon1) * (Math.PI / 180);

    const a = Math.sin(Δφ / 2) * Math.sin(Δφ / 2) +
        Math.cos(φ1) * Math.cos(φ2) *
        Math.sin(Δλ / 2) * Math.sin(Δλ / 2);

    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    return R * c;
}

export function orderNodesByProximity(nodes, startCoordinates, endCoordinates) {
    if (nodes.length === 0) return [];

    const startNode = nodes.find(node =>
        node.coordinates[0].toFixed(5) === startCoordinates[0].toFixed(5) &&
        node.coordinates[1].toFixed(5) === startCoordinates[1].toFixed(5)
    );

    const endNode = nodes.find(node =>
        node.coordinates[0].toFixed(5) === endCoordinates[0].toFixed(5) &&
        node.coordinates[1].toFixed(5) === endCoordinates[1].toFixed(5)
    );

    if (!startNode || !endNode) {
        throw new Error("Start or end node not found in the GeoJSON data.");
    }

    const orderedRoute = [startNode];
    const unvisitedNodes = nodes.filter(node => node !== startNode);

    let currentNode = startNode;

    while (unvisitedNodes.length > 0) {
        let closestNodeIndex = -1;
        let minDistance = Infinity;

        unvisitedNodes.forEach((node, index) => {
            const distance = getDistance(currentNode.coordinates, node.coordinates);
            if (distance < minDistance) {
                minDistance = distance;
                closestNodeIndex = index;
            }
        });

        const nextNode = unvisitedNodes.splice(closestNodeIndex, 1)[0];
        orderedRoute.push(nextNode);
        currentNode = nextNode;

        if (currentNode === endNode) break;
    }

    return orderedRoute.map(node => node.coordinates);
}


export function addAllTrails(map) {
    trailsInformation.forEach(trail => fetchAndDisplayGPXRoute(map, trail.id, trail.name, trail.color, trail.startPoint, trail.endPoint, trail.trailIcon, trail.displayName));
}

