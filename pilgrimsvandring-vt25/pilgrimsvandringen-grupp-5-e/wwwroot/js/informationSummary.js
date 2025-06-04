const huntingButton = document.getElementById("hunting-link");
const weatherButton = document.getElementById("weather-link");
const gearButton = document.getElementById("gear-link");
const natureButton = document.getElementById("nature-link");
const container = document.getElementById("info-container"); 

document.addEventListener("DOMContentLoaded", () => {
    huntingButton.click(); 
});

function createInfoContainers(container) {
    const smallInfoContainer = document.createElement("div");
    container.appendChild(smallInfoContainer);
    smallInfoContainer.classList.add("small-info-container");

    const infoContainer = document.createElement("div");
    smallInfoContainer.appendChild(infoContainer);
    infoContainer.classList.add("info-container");

    return { smallInfoContainer, infoContainer };
}

const createInfoSection = (headerText, paragraphText, parent) => {
    const section = document.createElement("div");
    section.classList.add("left-item");

    const header = document.createElement("h4");
    header.textContent = headerText;
    section.appendChild(header);

    const paragraph = document.createElement("p");
    paragraph.textContent = paragraphText;
    section.appendChild(paragraph);

    parent.appendChild(section);
};

const createHeader = (headerText) => {
    const header = document.createElement("h3");
    header.textContent = headerText;
    container.innerHTML = ""; 
    container.appendChild(header);
};

function createRightItem(headerText, listItems, parentContainer) {
    const rightItem = document.createElement("div");
    parentContainer.appendChild(rightItem);
    rightItem.classList.add("right-item");

    const listHeader = document.createElement("h4");
    rightItem.appendChild(listHeader);
    listHeader.textContent = headerText;
    listHeader.style.color = 'white';

    const listContainer = document.createElement("div");
    rightItem.appendChild(listContainer);
    listContainer.classList.add("list-container");

    listItems.forEach(item => {
        const listItem = document.createElement("p");
        listContainer.appendChild(listItem);
        listItem.classList.add("list-item");
        listItem.textContent = item;
    });
    return { rightItem, listContainer };
}

async function getSpottedAnimals() {
    const body = {
        "geographics": {
            "areas": [ {  "areaType": "County", "featureId": "23"} ]
        },
        "date": {
            "startDate": "2025-01-01", "endDate": "2025-02-13", "dateFilterType": "OverlappingStartDateAndEndDate"
        },
        "taxon": {
            "ids": [4000107],
            "includeUnderlyingTaxa": true,
            "taxonListOperator": "Filter"
        }
    };

    try {
        const response = await fetch('https://api.artdatabanken.se/species-observation-system/v1/Observations/Search?skip=0&take=100&validateSearchFilter=false&translationCultureCode=sv-SE&sensitiveObservations=false', {
            method: 'POST',
            body: JSON.stringify(body),
            headers: {
                'X-Api-Version': '1.5',
                'Content-Type': 'application/json',
                'Cache-Control': 'no-cache',
                'Ocp-Apim-Subscription-Key': '58b55cda916d480a8a23773a69a25115',
            }
        });

        if (response.ok) {
            const data = await response.json();
            return data;
        } else {
            console.error('Något gick fel:', response.status);
            return null;
        }
    } catch (error) {
        console.error('Något gick fel med fetch:', error);
        return null;
    }
}

huntingButton.addEventListener('click', () => {
    container.innerHTML = "";
    createHeader(huntingButton.textContent);

    const { smallInfoContainer, infoContainer } = createInfoContainers(container);

    createInfoSection("Jakt och allemansrätten", "I Sverige är jakt en reglerad aktivitet som styrs av jaktlagstiftningen och viltvårdsförordningen. Även om allemansrätten ger frihet att vistas i naturen, innebär den inte rätt att jaga eller störa vilt.", infoContainer);
    createInfoSection("Jaktsäsonger och säkerhet", "Under hösten och vintern pågår många jaktperioder, särskilt för älg och rådjur. Jakten kan ske i stora områden, och det är bra att vara uppmärksam på jaktlagens skyltar eller informera sig om jakt i området.", infoContainer);
    createInfoSection("Hundar och jaktområden", "Om du har med dig hund i skogen ska den vara kopplad mellan 1 mars och 20 augusti för att skydda viltet. Under jaktsäsongen kan lösa hundar uppfattas som en störning av jägare, så det är bäst att hålla hunden under kontroll.", infoContainer);
    createInfoSection("Jaktförbud och skyddade områden", "I nationalparker och naturreservat kan särskilda jaktregler gälla, och ibland råder totalförbud mot jakt. Kontrollera lokala regler innan du ger dig ut i ett område.", infoContainer);
    createInfoSection("Respekt och samspel", "Om du stöter på jägare i skogen, visa respekt genom att hålla avstånd och undvika att gå rakt igenom jaktmarker. Om du hör skott eller ser jaktskyltar kan det vara klokt att välja en annan rutt.", infoContainer);

    const huntingData = [
        { animal: "Älg", season: "30 jun - 1 okt" },
        { animal: "Vildsvin", season: "1 aug - 31 jan" },
        { animal: "Rådjur", season: "16 aug - 31 jan" },
        { animal: "Bock", season: "16 aug - 31 dec" },
        { animal: "Hare", season: "1 jul - 28 feb" },
        { animal: "Fasan", season: "1 okt - 28 feb" }
    ];

    createRightItem("Jakttider", [
        "Älg: 30 jun - 1 okt", 
        "Vildsvin: 1 aug - 31 jan", 
        "Rådjur: 16 aug - 31 jan", 
        "Bock: 16 aug - 31 dec", 
        "Hare: 1 jul - 28 feb", 
        "Fasan: 1 okt - 28 feb"
    ], smallInfoContainer);
});

weatherButton.addEventListener('click', () => {
    container.innerHTML = "";
    createHeader(weatherButton.textContent);

    const weatherSections = [
        ["Snö och kyla även på sommaren", "Även om det är sommar kan det fortfarande vara snö på högre högar, och det kan till och med snöa i juni, särskilt i fjällen. Var beredd på att vädret kan förändras snabbt, och att det kan bli mycket kallt, även om det är varmt på låga högar."],
        ["Vandring på is", "I vissa delar av Sverige, särskilt i norr, kan du gå på is fram till april och ibland även i maj, beroende på vädret. Tänk på att isen kan vara mycket osäker, och det är viktigt att vara försiktig när du vandrar över sjöar eller floder."],
        ["Midnattssol och Vintermörker", "I de norra delarna av Sverige kan du uppleva midnattssol under sommaren, vilket innebär att det är ljust dygnet runt. Under vintermånaderna kan det vara mörkt nästan hela dygnet. Planera din vandring utifrån detta och ta med dig tillräcklig belysning om du ska vara ute på kvällarna."]
    ];

    const weatherInfoContainer = document.createElement("div");
    container.appendChild(weatherInfoContainer);
    weatherInfoContainer.classList.add("weather-info-container");

    weatherSections.forEach(([header, text]) => {
        createInfoSection(header, text, weatherInfoContainer);
    });
});

gearButton.addEventListener('click', () => {
    container.innerHTML = "";
    createHeader(gearButton.textContent);

    const gearInfo = [
        ["Välj rätt utrustning", `Kläder och skor ska tåla vind och väta. Kläderna bör kunna bäras i flera skikt för att enkelt anpassas efter väder och temperatur. Bra utrustning behöver inte vara dyr och en klokt packad ryggsäck bör inte vara för tung. På Fjällsäkerhetsrådets webbplats finns checklistor över lämplig utrustning för olika aktiviteter i olika typer av fjällmiljöer.`],
        ["Meddela färdväg och beräknad återkomst", `Det är viktigt att någon känner till er planerade färdväg och när ni räknar med att komma tillbaka. Meddela en vän, en släkting eller någon annan som kan slå larm om ni inte skulle återvända som planerat.`],
        ["Anpassa din fjälltur efter vädret", `Vädret kan slå om hastigt i fjällen. Ta del av lokala och aktuella väderprognoser via radion eller SMHI:s webbplats. Vädertjänster erbjuds även som applikationer för mobiltelefoner. Respektera alltid utfärdade fjällvädervarningar. Den 15 december börjar lavinprognoser att publiceras.`],
        ["Följ markerade leder", `Vinterlederna öppnar normalt först i februari. Det finns hundratals mil markerade vinterleder i fjällen, med bland annat avståndsmarkeringar, övernattningsstugor och hjälptelefoner. De flesta gör klokt i att följa lederna, och är ett säkrare alternativ om ni av någon anledning skulle behöva hjälp.`],
        ["Ta med karta och kompass", `Se till att ha med en karta som är aktuell. Kompass behövs framförallt utanför markerade leder. Användning av kompass kräver dock kunskaper – instruktioner finns på Fjällsäkerhetsrådets webbplats. Komplettera gärna med GPS eller mobila applikationer vilka erbjuder möjlighet till att kunna orientera sig, men tänk på att batterier laddas ur snabbt när det är kallt.`]
    ];

    const { smallInfoContainer, infoContainer } = createInfoContainers(container);

    gearInfo.forEach(([header, text]) => {
        const section = createInfoSection(header, text, infoContainer);
    });

    createRightItem("Packlista", [
        "Karta",
        "Kompass",
        "Ficklampa",
        "Vattenflaska",
        "Stövlar"
    ], smallInfoContainer);
})

natureButton.addEventListener('click', async () => {
    container.innerHTML = "";
    createHeader(natureButton.textContent);

    const { smallInfoContainer, infoContainer } = createInfoContainers(container);

    const leftItem = document.createElement("div");
    infoContainer.appendChild(leftItem);
    leftItem.classList.add("left-item-nature");

    const infoHeader = document.createElement("h4");
    leftItem.appendChild(infoHeader);
    infoHeader.textContent = "Renar på fjället";
    infoHeader.style.color = 'white';

    const infoText = document.createElement("p");
    leftItem.appendChild(infoText);
    infoText.textContent = "Som besökare på fjället och i alla områden där renskötsel bedrivs är det viktigt att du inte stör eller skrämmer renen.";

    const infoList = document.createElement("ul");
    leftItem.appendChild(infoList);

    const tips = [
        "När du ser renar på fjället, stanna upp, sätt dig ner och var tyst. Låt renarna passera i sin egen takt.",
        "Gå aldrig rakt mot en betande ren – ta en omväg om det behövs.",
        "Drar sig flocken undan? Då har du redan kommit för nära.",
        "Lyfter renen på huvudet och svansen samtidigt som den spetsar öronen? Det är ett tydligt tecken på stress och att du bör backa.",
        "Håll alltid hunden kopplad och nära intill dig. Renen upplever instinktivt hunden som ett hot.",
        "Fråga gärna på närmaste turistbyrå, fjällstation eller fjällstuga var renarna befinner sig just nu och undvik de områdena.",
        "Visa även respekt och hänsyn mot renskötarna. Håll dig ur vägen och låt dem sköta sitt jobb. Du befinner dig på deras arbetsplats."
    ];

    tips.forEach(tip => {
        const listItem = document.createElement("li");
        listItem.textContent = tip;
        infoList.appendChild(listItem);
    });

    // TODO split into smaller functions
    async function initializeMap() {
       
        let animals = await getSpottedAnimals();
        console.log(animals.records);

        const dangerousAnimals = ["älg", "björn", "varg"];
        const filteredAnimals = animals.records.filter(record =>
            dangerousAnimals.includes(record.taxon.vernacularName)
        );

        const animalLocations = [];

        const { rightItem, listContainer } = createRightItem(
            "Observerade djur att vara uppmärksam på", 
            [], 
            smallInfoContainer 
        );

        filteredAnimals.forEach(record => {
            const listItem = document.createElement("p");
            listContainer.appendChild(listItem);
            listItem.classList.add("list-item");
            listItem.textContent = `${record.taxon.vernacularName}, ${record.location.municipality.name}`;

            const lat = parseFloat(record.location?.decimalLatitude);
            const lon = parseFloat(record.location?.decimalLongitude);

            if (!isNaN(lat) && !isNaN(lon)) {
                const spottedDate = new Date(record.event.startDate);
                const formattedDate = spottedDate.toLocaleString();

                animalLocations.push({
                    lat,
                    lon,
                    animalName: record.taxon.vernacularName,
                    location: record.location.municipality?.name,
                    spottedDate: formattedDate
                });
            } else {
                console.warn(`Invalid coordinates for ${record.taxon.vernacularName}`);
            }
        });

        const showOnMapButton = document.createElement("button");
        rightItem.appendChild(showOnMapButton);
        showOnMapButton.textContent = "Visa alla på karta";
        showOnMapButton.classList.add("show-map-button");

        let mapContainer = null;
        let newMap = null;
                
        showOnMapButton.addEventListener('click', () => {
            if (mapContainer) {
                rightItem.removeChild(mapContainer);
                mapContainer = null;
                newMap = null; 
            } else {
                mapContainer = document.createElement("div");
                mapContainer.style.width = "100%";
                mapContainer.style.height = "400px";
                rightItem.appendChild(mapContainer); 

                newMap = L.map(mapContainer).setView([62.9, 13.53], 8);

                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    maxZoom: 19,
                    attribution: '© OpenStreetMap contributors'
                }).addTo(newMap);

                animalLocations.forEach(location => {
                    L.marker([location.lat, location.lon])
                        .addTo(newMap)
                        .bindPopup(`<b>${location.animalName}</b><br>${location.location}<br>Observerad: ${location.spottedDate}`);
                });

                if (animalLocations.length > 0) {
                    const bounds = L.latLngBounds(animalLocations.map(loc => [loc.lat, loc.lon]));
                    newMap.fitBounds(bounds);
                }
            }
        });
    }

    initializeMap();

});
