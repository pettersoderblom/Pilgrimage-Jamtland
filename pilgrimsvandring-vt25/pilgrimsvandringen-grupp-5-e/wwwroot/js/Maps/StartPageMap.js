import { fetchAndDisplayRoute, addAllTrails } from "./MapRoutes.js";


const map = L.map('start-page-map').setView([62.9, 13.53], 8);

// Add OpenStreetMap tiles
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '© OpenStreetMap contributors'
}).addTo(map);


fetchAndDisplayRoute(map);
addAllTrails(map); 