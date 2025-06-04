
const forwardButton = document.getElementById("forward-button")
const backButton = document.getElementById("back-button")
const cards = document.querySelectorAll(".stage-item")
const itemList = document.getElementById("flex-container-stages")

const widthOfitemList = itemList.offsetWidth;
let cardWidth = 0;
const gap = 10; 


cards.forEach(card => {
    cardWidth = card.offsetWidth;
});

const numberOfCardsInCotainer = Math.floor(widthOfitemList / (cardWidth + gap)); 

let scrollAmount = numberOfCardsInCotainer * cardWidth; 


forwardButton.addEventListener("click", () => {
    itemList.scrollLeft += scrollAmount
backButton.style.display="block"
})

backButton.addEventListener("click", () => {
    itemList.scrollLeft -= scrollAmount
})

const myDiv = document.querySelector(".stage-item")
const bigDiv = document.getElementById("flex-container-stages")
if (myDiv === null) {
    backButton.style.display = "none"
    forwardButton.style.display = "none"
    bigDiv.style.display = "none"
}


const div = document.createElement("div"); 
div.className = "stage-item"; 