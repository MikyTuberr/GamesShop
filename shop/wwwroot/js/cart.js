var cartItemCountString = getCookie('cartItemCount');
var cartItemCount = cartItemCountString ? parseInt(cartItemCountString) : 0;
updateCartItemCount(cartItemCount);

$(document).ready(function () {
    // Obs³uga klikniêcia przycisku "Add to Cart" z polem wprowadzania iloœci
    $('[id^="m_addToCartButton"]').click(function () {
        var itemId = $(this).data('id');
        var cartItemsString = getCookie('cartItems');
        var cartItems = cartItemsString ? JSON.parse(cartItemsString) : [];

        // Pobierz iloœæ wpisan¹ w polu inputQuantity
        var quantity = parseInt($('#inputQuantity').val());

        // SprawdŸ, czy quantity jest null lub NaN
        if (!quantity || isNaN(quantity)) {
            // Ustaw wartoœæ quantity na 1, jeœli nieprawid³owa
            quantity = 1;
        }

        // Dodaj okreœlon¹ iloœæ przedmiotów do koszyka
        for (var i = 0; i < quantity; i++) {
            cartItems.push(itemId);
        }

        // Zapisz koszyk w ciasteczku
        setCookie('cartItems', JSON.stringify(cartItems), 7);

        // Zwiêksz licznik o iloœæ dodanych przedmiotów i zapisz go w ciasteczku
        var cartItemCountString = getCookie('cartItemCount');
        var cartItemCount = cartItemCountString ? parseInt(cartItemCountString) : 0;
        cartItemCount += quantity;
        setCookie('cartItemCount', cartItemCount.toString(), 7);

        // Aktualizuj licznik przedmiotów w koszyku
        updateCartItemCount(cartItemCount);

        alert(quantity + ' items added to cart!');
    });

    // Obs³uga klikniêcia przycisku "Add to Cart" bez pola wprowadzania iloœci
    $('[id^="addToCartButton"]').click(function () {
        var itemId = $(this).data('id');
        var cartItemsString = getCookie('cartItems');
        var cartItems = cartItemsString ? JSON.parse(cartItemsString) : [];

        cartItems.push(itemId);

        // Zapisz koszyk w ciasteczku
        setCookie('cartItems', JSON.stringify(cartItems), 7);

        // Zwiêksz licznik o 1 i zapisz go w ciasteczku
        var cartItemCountString = getCookie('cartItemCount');
        var cartItemCount = cartItemCountString ? parseInt(cartItemCountString) : 0;
        cartItemCount++;
        setCookie('cartItemCount', cartItemCount.toString(), 7);

        // Aktualizuj licznik przedmiotów w koszyku
        updateCartItemCount(cartItemCount);

        alert('Item added to cart!');
    });

    // Aktualizacja licznika w koszyku
    function updateCartItemCount(count) {
        $('.badge.bg-dark.text-white.ms-1.rounded-pill').text(count);
    }
});


// Funkcja do ustawiania ciasteczka
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }

    document.cookie = name + "=" + (value || "") + expires + "; path=/; SameSite=None; Secure";
}


// Funkcja do pobierania ciasteczka
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

// Funkcja do aktualizacji licznika przedmiotów w koszyku
function updateCartItemCount(count) {
    $('.badge.bg-dark.text-white.ms-1.rounded-pill').text(count);
}
