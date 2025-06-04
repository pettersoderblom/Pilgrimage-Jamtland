import { redFlag, yellowFlag, blueFlag } from "../js/Maps/Markers.js";
import { fetchAndDisplayGPXRoute, fetchAndDisplayRoute } from "../js/Maps/MapRoutes.js";
import { DisplayPointOfInterests, addChekBoxes } from "../js/Maps/Poi.js";


let trailsInformation = [
    { id: 4, name: 'Indalsleden', color: 'blue', trailIcon: blueFlag, startPoint: [62.4855311, 17.3244924], endPoint: [63.3160351, 12.1017855], displayName: 'Indalsleden' },
    { id: 2, name: 'Karboleleden', color: 'yellow', trailIcon: yellowFlag, startPoint: [60.565463, 17.4374], endPoint: [62.80033, 13.056407], displayName: 'Kårböleleden' },
    { id: 3, name: 'st-olovsleden', color: 'red', trailIcon: redFlag, startPoint: [62.386808961183199, 17.315430391312599], endPoint: [63.427208, 10.396927], displayName: 'S:t Olovsleden' },
    { id: 1, name: 'jamt-norge-leden', displayName: 'Jämt Norge vägen' }
];

// Initialize the map
const map = L.map('trail').setView([62.9, 13.53], 8);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '© OpenStreetMap contributors'
}).addTo(map);


function initializeMap() {
    const mapContainer = document.getElementById("trail");

    if (mapContainer && mapContainer.dataset.trailId) {
        const selectedTrailId = parseInt(mapContainer.dataset.trailId, 10); 

        const selectedTrail = trailsInformation.find(trail => trail.id === selectedTrailId);

        if (selectedTrail) {
            if (selectedTrail.id === 1) {
                fetchAndDisplayRoute(map);
            } else {
                fetchAndDisplayGPXRoute(map, selectedTrail.id, selectedTrail.name, selectedTrail.color, selectedTrail.startPoint, selectedTrail.endPoint, selectedTrail.trailIcon, selectedTrail.displayName);
            }
        }

        
        DisplayPointOfInterests(map, selectedTrailId).then(() => {
            addChekBoxes(map);
        });
    }
}

initializeMap();
