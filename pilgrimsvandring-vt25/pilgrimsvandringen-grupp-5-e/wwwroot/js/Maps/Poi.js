import { vadstalle, vindskydd, utedass, varning, fartyg, shop, church } from "../Maps/Markers.js";

const existingCategories = new Set();

export let categoryMarkers = {
    sleep: [],
    wc: [],
    boat: [],
    waterCrossing: [],
    varningArray: [], 
    churches: [],
    shops: []
};

const categoryNames = { //changes the display name for the chkboxes
    sleep: "Vindskydd",
    wc: "Dass",
    boat: "Båtöverfart",
    waterCrossing: "Vadställe",
    varningArray: "Varning",
    churches: "Kyrka",
    shops: "Affär"
};

// Define projections
const sweref99tm = "+proj=utm +zone=33 +ellps=GRS80 +units=m +no_defs";
const wgs84 = "+proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs";

/**
 * Function to convert coordinates
 * @param {string} easting
 * @param {string} northing
 * @returns converted latitude longitude
 */
export function convertToLatLng(easting, northing) {
    const [longitude, latitude] = proj4(sweref99tm, wgs84, [easting, northing]);
    return { lat: latitude, lng: longitude };
}

export function getCategoryIcon(category) {
    switch (category.toLowerCase()) {
        case 'vadställe':
            return vadstalle;
        case 'vindskydd':
            return vindskydd;
        case 'dass':
            return utedass;
        case 'varning':
            return varning;
        case 'slogbod':
            return vindskydd;
        case 'båtöverfart':
            return fartyg;
        case 'färjeläge':
            return fartyg;
        case 'pån':
            return fartyg;
        case 'kyrka':
            return church;
        case 'affär':
            return shop;
    }
}
// TODO split up into smaller functions
export async function DisplayPointOfInterests(map, selectedTrailId = null) { 
    try {
        const response = await fetch('/PointsOfInterests/GetAll');
        if (!response.ok) throw new Error('Failed to load POIs.');

        const points = await response.json();

        let filteredPoints; 

        if (selectedTrailId) { 
            // Only show POIs for this specific trail 
            filteredPoints = points.filter(point => point.trailId === selectedTrailId); 
        } else { 
            // Show all POIs (default map) 
            filteredPoints = points; 
        } 
     

        for (const point of filteredPoints) {
            const easting = parseFloat(point.longitude.substring(1));
            const northing = parseFloat(point.latitude.substring(1));

            const icon = getCategoryIcon(point.category);
            if (!icon) continue; //Skips unknown categories

            const { lat, lng } = convertToLatLng(easting, northing);

            const { temperature, weatherCode } = await getWeather(lat, lng);
            const weatherDescription = weatherCode !== null ? getWeatherDescription(weatherCode) : "Väder ej tillgängligt";

            existingCategories.add(point.category); // Add category to existingCategories

            // Add marker to the map
            const marker = L.marker([lat, lng], { icon: icon }).addTo(map);
            marker.bindPopup(`
                <b>${point.name}</b><br>
                ${point.category}<br>
                ${point.description}<br>
                ${weatherDescription}<br>
                ${temperature} celsius
            `);

            addToCategoryList(point, marker);
        }

    } catch (error) {
        console.error('Error fetching POIs:', error);
        document.body.innerText = 'Error fetching POIs';
    }
}



export function addToCategoryList(point, marker) {
    const category = point.category.toLowerCase();


    switch (category) {
        case 'vadställe':
            categoryMarkers['waterCrossing'].push(marker);
            break;
        case 'vindskydd':
            categoryMarkers['sleep'].push(marker);
            break;
        case 'dass':
            categoryMarkers['wc'].push(marker);
            break;
        case 'varning':
            categoryMarkers['varningArray'].push(marker);
            break;
        case 'slogbod':
            categoryMarkers['sleep'].push(marker);
            break;
        case 'båtöverfart':
            categoryMarkers['boat'].push(marker);
            break;
        case 'färjeläge':
            categoryMarkers['boat'].push(marker);
            break;
        case 'pån':
            categoryMarkers['boat'].push(marker);
            break;
        case 'kyrka':
            categoryMarkers['churches'].push(marker);
            break;
        case 'affär':
            categoryMarkers['shops'].push(marker);
            break;
    }
}

export function toggleCategory(map, category) {
    const markers = categoryMarkers[category];
    if (!markers) return; // If no markers exist for this category, do nothing

    markers.forEach(marker => {
        if (map.hasLayer(marker)) {
            map.removeLayer(marker);
        } else {
            marker.addTo(map);
        }
    });
}

async function getWeather(latitude, longitude) {
    try {
        const response = await fetch(`/Weather/GetWeatherByCords?latitude=${latitude}&longitude=${longitude}`);

        if (!response.ok) {
            throw new Error(`Weather API error`);
        }

        const weatherData = await response.json();

        return {
            temperature: weatherData?.current_weather?.temperature ?? "N/A",
            weatherCode: weatherData?.current_weather?.weathercode ?? null
        };

    } catch (error) {
        console.error("Failed to fetch weather data:", error);
        return { temperature: "N/A", weatherCode: null };
    }
}

// TODO check if it can be replaced by a similiar weatherfunction in weathersummary.js
function getWeatherDescription(weathercode) {
    const weatherDescription = {
        0: "Klart väder ☀️",
        1: "Mestadels klart 🌤️",
        2: "Delvis molnigt 🌥️",
        3: "Mulet ☁️",
        45: "Dimma 🌫️",
        48: "Underkyld dimma ❄️🌫️",
        51: "Lätt duggregn 🌦️",
        53: "Måttligt duggregn 🌧️",
        55: "Kraftigt duggregn 🌧️",
        61: "Lätt regn 🌦️",
        63: "Måttligt regn 🌧️",
        65: "Kraftigt regn 🌧️🌊",
        71: "Lätt snöfall 🌨️",
        73: "Måttligt snöfall 🌨️",
        75: "Kraftigt snöfall ❄️❄️",
        80: "Lätta regnskurar 🌦️",
        81: "Måttliga regnskurar 🌧️",
        82: "Kraftiga regnskurar 🌧️🌊",
        95: "Åskväder ⛈️",
        96: "Åskväder med hagel ⛈️🌨️",
        99: "Kraftigt åskväder med hagel ⚡❄️"
    };

    return weatherDescription[weathercode] || "Okänt väder 🤷‍";
}

export function addChekBoxes(map) {
    const container = document.getElementById("checkboxes-icons");
    container.innerHTML = "";

    // Find categories that actually have markers
    const categoriesWithMarkers = Object.keys(categoryMarkers).filter(category => categoryMarkers[category].length > 0);

    categoriesWithMarkers.forEach(category => {
        const categoryLabel = categoryNames[category] || category; //Shows displayname

        const checkboxWrapper = document.createElement("div");
        checkboxWrapper.classList.add("checkbox-wrapper-1");

        const checkboxInput = document.createElement("input");
        checkboxInput.id = `${category}`;
        checkboxInput.classList.add("substituted");
        checkboxInput.type = "checkbox";
        checkboxInput.checked = true;

        const label = document.createElement("label");
        label.setAttribute("for", checkboxInput.id);
        label.innerText = categoryLabel;

        checkboxWrapper.appendChild(checkboxInput);
        checkboxWrapper.appendChild(label);
        container.appendChild(checkboxWrapper);

        checkboxInput.addEventListener('change', function () {
            toggleCategory(map, category);
        });
    });
}