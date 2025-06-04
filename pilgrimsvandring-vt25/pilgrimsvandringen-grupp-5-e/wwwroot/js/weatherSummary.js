document.addEventListener("DOMContentLoaded", () => {
    setupDropdown();
    setupTrailSelection();
});

/**
 * Dropdown over trails
 */
function setupDropdown() {
    const button = document.getElementById("myButton");
    const dropdown = document.getElementById("myDropdown");

    button.addEventListener("click", () => dropdown.classList.toggle("show"));

    window.addEventListener("click", (event) => {
        if (!event.target.closest(".dropbtn")) {
            dropdown.classList.remove("show");
        }
    });
}

function setupTrailSelection() {
    document.querySelectorAll(".trail-link").forEach(link => {
        link.addEventListener("click", () => {
            const trailIndex = link.dataset.trailIndex;
            const trailName = link.querySelector("h3").textContent;
            document.getElementById("myButton").innerHTML = `${trailName} <img src="/icons/mainpage/dropdown.png" />`;
            toggleWeatherDetails(trailIndex);
        });
    });
}


const weatherCodeElements = document.querySelectorAll(".weather-code");

/**
 * Weather descriptions and icons mapping
 */
const wmoDescriptions = {
    "0": createWeatherEntry("Soligt", "Klart", "wsymbol_0001_sunny.png", "wsymbol_0008_clear_sky_night.png"),
    "1": createWeatherEntry("Mestadels soligt", "Mestadels klart", "wsymbol_0002_sunny_intervals.png", "wsymbol_0041_partly_cloudy_night.png"),
    "2": createWeatherEntry("Delvis molnigt", "Delvis molnigt", "wsymbol_0043_mostly_cloudy.png", "wsymbol_0044_mostly_cloudy_night.png"),
    "3": createWeatherEntry("Molnigt", "Molnigt", "wsymbol_0003_white_cloud.png", "wsymbol_0042_cloudy_night.png"),
    "45": createWeatherEntry("Dimma", "Dimma", "wsymbol_0007_fog.png", "wsymbol_0064_fog_night.png"),
    "48": createWeatherEntry("Rimfrostdimma", "Rimfrostdimma", "wsymbol_0006_mist.png", "wsymbol_0063_mist_night.png"),
    "51": createWeatherEntry("Lätt duggregn", "Lätt duggregn", "wsymbol_0048_drizzle.png", "wsymbol_0066_drizzle_night.png"),
    "53": createWeatherEntry("Duggregn", "Duggregn", "wsymbol_0048_drizzle.png", "wsymbol_0066_drizzle_night.png"),
    "55": createWeatherEntry("Kraftigt duggregn", "Kraftigt duggregn", "wsymbol_0048_drizzle.png", "wsymbol_0066_drizzle_night.png"),
    "56": createWeatherEntry("Lätt underkylt duggregn", "Lätt underkylt duggregn", "wsymbol_0013_sleet_showers.png", "wsymbol_0029_sleet_showers_night.png"),
    "57": createWeatherEntry("Underkylt duggregn", "Underkylt duggregn", "wsymbol_0013_sleet_showers.png", "wsymbol_0029_sleet_showers_night.png"),
    "61": createWeatherEntry("Lätt regn", "Lätt regn", "wsymbol_0009_light_rain_showers.png", "wsymbol_0025_light_rain_showers_night.png"),
    "63": createWeatherEntry("Regn", "Regns", "wsymbol_0018_cloudy_with_heavy_rain.png", "wsymbol_0034_cloudy_with_heavy_rain_night.png"),
    "65": createWeatherEntry("Kraftigt regn", "Kraftigt regn", "wsymbol_0018_cloudy_with_heavy_rain.png", "wsymbol_0034_cloudy_with_heavy_rain_night.png"),
    "66": createWeatherEntry("Lätt underkylt regn", "Lätt underkylt regn", "wsymbol_0013_sleet_showers.png", "wsymbol_0029_sleet_showers_night.png"),
    "67": createWeatherEntry("Underkylt regn", "Underkylt regn", "wsymbol_0021_cloudy_with_sleet.png", "wsymbol_0037_cloudy_with_sleet_night.png"),
    "71": createWeatherEntry("Lätt snöfall", "Lätt snöfall", "wsymbol_0011_light_snow_showers.png", "wsymbol_0027_light_snow_showers_night.png"),
    "73": createWeatherEntry("Snöfall", "Snöfall", "wsymbol_0020_cloudy_with_heavy_snow.png", "wsymbol_0036_cloudy_with_heavy_snow_night.png"),
    "75": createWeatherEntry("Kraftigt snöfall", "Kraftigt snöfall", "wsymbol_0020_cloudy_with_heavy_snow.png", "wsymbol_0036_cloudy_with_heavy_snow_night.png"),
    "77": createWeatherEntry("Snökorn", "Snökorn", "wsymbol_0011_light_snow_showers.png", "wsymbol_0027_light_snow_showers_night.png"),
    "80": createWeatherEntry("Lätta regnskurar", "Lätta regnskurar", "wsymbol_0009_light_rain_showers.png", "wsymbol_0025_light_rain_showers_night.png"),
    "81": createWeatherEntry("Regnskurar", "Regnskurar", "wsymbol_0018_cloudy_with_heavy_rain.png", "wsymbol_0034_cloudy_with_heavy_rain_night.png"),
    "82": createWeatherEntry("Kraftiga regnskurar", "Kraftiga regnskurar", "wsymbol_0018_cloudy_with_heavy_rain.png", "wsymbol_0034_cloudy_with_heavy_rain_night.png"),
    "85": createWeatherEntry("Lätta snöbyar", "Lätta snöbyar", "wsymbol_0011_light_snow_showers.png", "wsymbol_0027_light_snow_showers_night.png"),
    "86": createWeatherEntry("Snöbyar", "Snöbyar", "wsymbol_0020_cloudy_with_heavy_snow.png", "wsymbol_0036_cloudy_with_heavy_snow_night.png"),
    "95": createWeatherEntry("Åskväder", "Åskväder", "wsymbol_0024_thunderstorms.png", "wsymbol_0040_thunderstorms_night.png"),
    "96": createWeatherEntry("Underkylt åskväder", "Underkylt åskväder", "wsymbol_0024_thunderstorms.png", "wsymbol_0040_thunderstorms_night.png"),
    "99": createWeatherEntry("Underkylt åskväder", "Underkylt åskväder", "wsymbol_0024_thunderstorms.png", "wsymbol_0040_thunderstorms_night.png"),
};

function createWeatherEntry(dayDesc, nightDesc, dayImg, nightImg) {
    return {
        day: { description: dayDesc, image: `/images/WeatherSymbols/${dayImg}` },
        night: { description: nightDesc, image: `/images/WeatherSymbols/${nightImg}` },
    };
}

function translateWMO(weatherCode, timeOfDay) {
    const weather = wmoDescriptions[weatherCode];
    if (weather) {
        const weatherInfo = weather[timeOfDay];
        if (weatherInfo) {
            return weatherInfo;
        }
    }
    return { description: "Okänt väder", image: "/images/WeatherSymbols/wsymbol_0999_unknown.png" };
}

const currentHour = new Date().getHours();
function getTimeOfDay() {
   
    return (currentHour >= 6 && currentHour < 18) ? 'day' : 'night';
}

weatherCodeElements.forEach(element => {
    const weatherCode = parseInt(element.innerText, 10);

    const timeOfDay = getTimeOfDay();
    const weatherInfo = translateWMO(weatherCode, timeOfDay);

    element.innerHTML = `
     <img src="${weatherInfo.image}" alt="${weatherInfo.description}" />
        <p>${weatherInfo.description}</p>
       
    `;
});

toggleWeatherDetails(0);


function toggleWeatherDetails(trailIndex) {
 
    let allTrails = document.querySelectorAll('.weather-start-end-container');
    allTrails.forEach(function (trail) {
        trail.style.display = 'none';
    });

   
    let selectedTrail = document.getElementById('trail-' + trailIndex);
    selectedTrail.style.display = 'flex';
}

let toggleButtons = document.querySelectorAll(".toggle-details");

toggleButtons.forEach(function (button) {
    button.addEventListener("click", function () {
        const trailIndex = this.getAttribute('data-trail-index');
        const point = this.getAttribute('data-point');
        toggleWeatherPointDetails(trailIndex, point);
    });
});

function toggleWeatherPointDetails(trailIndex, point) {
    console.log("Button clicked!");
    var detailsElement = document.getElementById(point + '-weather-details-' + trailIndex);
    console.log(detailsElement);


    if (detailsElement.style.display === 'none' || detailsElement.style.display === '') {
        detailsElement.style.display = 'block';
    } else {
        detailsElement.style.display = 'none';
    }
}
