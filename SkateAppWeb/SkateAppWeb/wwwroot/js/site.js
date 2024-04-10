
function toggleCurrency() {
    // Get all elements with the class 'price'
    var priceElements = document.querySelectorAll('.price');

    // Loop through each price element
    priceElements.forEach(function (priceElement) {
        // Get the current price and the original price from the data attribute
        var currentPrice = parseFloat(priceElement.innerText.replace(/[^\d.]/g, ''));
        var originalPrice = parseFloat(priceElement.getAttribute('data-original-price'));

        // Check if the current currency is the original currency
        if (priceElement.getAttribute('data-is-converted') === 'true') {
            // If it's converted, switch back to the original currency
            priceElement.innerText = originalPrice.toFixed(1.96) + 'lv';
            priceElement.setAttribute('data-is-converted', 'false');
        } else {
            // If it's the original, convert to a different currency (e.g., USD)
            var exchangeRate = 0.5;  // Example exchange rate
            var convertedPrice = currentPrice * exchangeRate;
            priceElement.innerText = '€' + convertedPrice.toFixed(2);
            priceElement.setAttribute('data-is-converted', 'true');
        }
    });

    // Change the name of the button dynamically
    var button = document.getElementById('changeCurrencyBtn');
    var isConverted = priceElements[0].getAttribute('data-is-converted') === 'true';

    button.innerText = isConverted ? 'Euro' : 'Lv';
}

// Event listener for the button click
document.getElementById('changeCurrencyBtn').addEventListener('click', toggleCurrency); s

function toggleReviews() {
    var reviewsContainer = document.getElementById("reviewsContainer");

    if (reviewsContainer.style.display === "none" || reviewsContainer.style.display === "") {
        reviewsContainer.style.display = "block";
    } else {
        reviewsContainer.style.display = "none";
    }
}
