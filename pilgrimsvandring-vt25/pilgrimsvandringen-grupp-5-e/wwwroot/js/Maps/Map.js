import { addUserLocation, fetchAndDisplayRoute, addAllTrails } from "./MapRoutes.js";
import { DisplayPointOfInterests, addChekBoxes } from "./Poi.js";


const map = L.map('map').setView([62.9, 13.53], 8);

// Add OpenStreetMap tiles
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '© OpenStreetMap contributors'
}).addTo(map);


addUserLocation(map);
fetchAndDisplayRoute(map);
addAllTrails(map); 
DisplayPointOfInterests(map); 

DisplayPointOfInterests(map).then(() => {
    addChekBoxes(map);
});