document.addEventListener("DOMContentLoaded", function () {
    // ----------------------
    // Agregar al carrito
    // ----------------------
    const addToCartButtons = document.querySelectorAll(".add-to-cart");
    addToCartButtons.forEach(btn => {
        btn.addEventListener("click", () => {
            const product = {
                id: btn.getAttribute("data-id"),
                name: btn.getAttribute("data-name"),
                price: parseFloat(btn.getAttribute("data-price"))
            };
            addToCart(product);
        });
    });

    // ----------------------
    // Página: Carrito
    // ----------------------
    if (window.location.pathname.toLowerCase() === "/cart") {
        renderCart();
    }
    
    

    // ----------------------
    // Página: Checkout
    // ----------------------
    if (window.location.pathname.toLowerCase().includes("/cart/checkout")) {
        renderCheckoutSummary();
    }

    // ----------------------
    // Página: Registro
    // ----------------------
    document.getElementById("registerForm")?.addEventListener("submit", function (e) {
        e.preventDefault();
        const user = document.getElementById("regUser").value;
        const email = document.getElementById("regEmail").value;
        const pass = document.getElementById("regPass").value;
        const pass2 = document.getElementById("regPass2").value;

        if (pass !== pass2) {
            alert("Las contraseñas no coinciden ⚠️");
            return;
        }

        const account = { user, email, pass };
        localStorage.setItem("gg_user", JSON.stringify(account));
        alert("Cuenta registrada con éxito 🎉 Ahora inicia sesión.");
        window.location.href = "/Account/Login";
    });

    // ----------------------
    // Página: Login
    // ----------------------
    document.getElementById("loginForm")?.addEventListener("submit", function (e) {
        e.preventDefault();
        const username = document.getElementById("loginUser").value;
        const password = document.getElementById("loginPass").value;
        const account = JSON.parse(localStorage.getItem("gg_user"));

        if (!account || account.user !== username || account.pass !== password) {
            document.getElementById("loginMessage").textContent = "Usuario o contraseña incorrectos 😓";
        } else {
            localStorage.setItem("gg_logged", username);
            alert(`¡Bienvenido de nuevo, ${username}! 👾`);
            window.location.href = "/";
        }
    });

    // ----------------------
    // Página: Órdenes
    // ----------------------
    if (window.location.pathname.toLowerCase().includes("/orders")) {
        const user = localStorage.getItem("gg_logged");
        const orders = JSON.parse(localStorage.getItem("orders") || "[]");
        const list = document.getElementById("orders-list");

        if (!user) {
            list.innerHTML = "<p>Iniciá sesión para ver tus órdenes.</p>";
        } else {
            const userOrders = orders.filter(o => o.user === user);

            if (userOrders.length === 0) {
                list.innerHTML = "<p>No tenés órdenes aún 😴</p>";
            } else {
                userOrders.forEach(order => {
                    const productos = order.items.map(p => `<li>${p.name} x${p.quantity}</li>`).join("");
                    list.innerHTML += `
                        <div class="mb-3 p-3 border border-success rounded bg-dark text-light">
                            <strong>Orden #${order.id}</strong> - ${order.date}<br/>
                            Total: $${order.total.toFixed(2)}<br/>
                            Pago con: ${order.metodoPago}<br/>
                            Productos:
                            <ul>${productos}</ul>
                        </div>
                    `;
                });
            }
        }
    }
});

// ----------------------
// Carrito
// ----------------------

function getCart() {
    return JSON.parse(localStorage.getItem("cart")) || [];
}

function saveCart(cart) {
    localStorage.setItem("cart", JSON.stringify(cart));
}

function addToCart(product) {
    const cart = getCart();
    const existing = cart.find(p => p.id === product.id);
    if (existing) {
        existing.quantity += 1;
    } else {
        product.quantity = 1;
        cart.push(product);
    }
    saveCart(cart);
    alert(`${product.name} agregado al carrito 🎮`);
}

function renderCart() {
    const cart = getCart();
    const container = document.getElementById("cart-items");
    const totalSpan = document.getElementById("cart-total");
    container.innerHTML = "";

    let total = 0;

    if (cart.length === 0) {
        container.innerHTML = "<p>Tu carrito está vacío 💤</p>";
    } else {
        cart.forEach(p => {
            const subtotal = p.price * p.quantity;
            total += subtotal;

            const item = document.createElement("div");
            item.classList.add("mb-3");
            item.innerHTML = `
                <strong>${p.name}</strong><br/>
                Cantidad: ${p.quantity} - Subtotal: $${subtotal}<br/>
                <button class="btn btn-sm btn-danger" onclick="removeFromCart(${p.id})">Eliminar</button>
                <hr/>
            `;
            container.appendChild(item);
        });
    }

    totalSpan.textContent = total.toFixed(2);
}

function removeFromCart(id) {
    let cart = getCart();
    cart = cart.filter(p => p.id != id);
    saveCart(cart);
    renderCart();
}

function clearCart() {
    localStorage.removeItem("cart");
    // Solo renderizar si estás en /Cart
    if (window.location.pathname.toLowerCase() === "/cart") {
        renderCart();
    }
}

// ----------------------
// Checkout
// ----------------------

function renderCheckoutSummary() {
    const cart = getCart();
    const summary = document.getElementById("checkout-summary");

    if (!summary) return;

    if (cart.length === 0) {
        summary.innerHTML = "<p>🛒 Tu carrito está vacío.</p>";
    } else {
        let total = 0;
        summary.innerHTML = "<h4 class='text-light'>Resumen de tu compra:</h4>";
        cart.forEach(p => {
            total += p.price * p.quantity;
            summary.innerHTML += `
                <p class="text-light">${p.name} x${p.quantity} - $${(p.price * p.quantity).toFixed(2)}</p>
            `;
        });
        summary.innerHTML += `<hr/><strong class="text-neon">Total: $${total.toFixed(2)}</strong>`;
    }
}

function toggleCardFields() {
    const selected = document.getElementById("cardPayment").value;
    const fields = document.getElementById("cardFields");

    if (selected && selected !== "Seleccioná un banco") {
        fields.classList.remove("d-none");
        document.querySelectorAll('input[name="wallet"]').forEach(w => w.checked = false);
    } else {
        fields.classList.add("d-none");
    }
}

function clearCardSelection() {
    document.getElementById("cardPayment").selectedIndex = 0;
    document.getElementById("cardFields")?.classList.add("d-none");
}

function confirmOrder(e) {
    e.preventDefault();

    const cart = getCart();
    const user = localStorage.getItem("gg_logged");
    const messageBox = document.getElementById("checkout-message");

    const selectedCard = document.getElementById("cardPayment").value;
    const selectedWallet = document.querySelector('input[name="wallet"]:checked');

    if (!user) {
        alert("Iniciá sesión para continuar 🔐");
        return;
    }

    if (cart.length === 0) {
        alert("Tu carrito está vacío 😅");
        return;
    }

    let metodoPago = "";

    if (selectedCard && selectedCard !== "Seleccioná un banco") {
        const number = document.getElementById("cardNumber").value.trim();
        const cvv = document.getElementById("cardCVV").value.trim();
        const exp = document.getElementById("cardExp").value.trim();
        const name = document.getElementById("cardName").value.trim();

        if (!number || !cvv || !exp || !name) {
            alert("Completá todos los datos de la tarjeta 💳");
            return;
        }

        metodoPago = `${selectedCard} - ${name}`;
    } else if (selectedWallet) {
        metodoPago = selectedWallet.value;
    } else {
        alert("Seleccioná un método de pago 🤑");
        return;
    }

    const orders = JSON.parse(localStorage.getItem("orders") || "[]");
    const order = {
        id: Date.now(),
        user,
        items: cart,
        total: cart.reduce((sum, item) => sum + (item.price * item.quantity), 0),
        date: new Date().toLocaleString(),
        metodoPago
    };

    orders.push(order);
    localStorage.setItem("orders", JSON.stringify(orders));
    clearCart();

    if (messageBox) {
        messageBox.innerText = `✅ ¡Pedido confirmado! Método de pago: ${metodoPago}`;
    }

    document.getElementById("checkout-summary").innerHTML = "";
    document.getElementById("paymentForm").reset();
    document.getElementById("cardFields")?.classList.add("d-none");
}
