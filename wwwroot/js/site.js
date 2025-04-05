// Filtrar productos por categoría y precio
document.getElementById('filterForm').addEventListener('submit', function (e) {
    e.preventDefault();
  
    var category = document.getElementById('categoryFilter').value;
    var price = document.getElementById('priceFilter').value;
  
    // Filtrar productos según la categoría y el precio
    var filteredProducts = products.filter(function (product) {
      var matchesCategory = category ? product.category === category : true;
      var matchesPrice = false;
  
      if (price) {
        var priceRange = price.split('-');
        var minPrice = parseInt(priceRange[0]);
        var maxPrice = priceRange[1] ? parseInt(priceRange[1]) : Infinity;
        matchesPrice = product.price >= minPrice && product.price <= maxPrice;
      } else {
        matchesPrice = true;
      }
  
      return matchesCategory && matchesPrice;
    });
  
    // Mostrar productos filtrados
    displayProducts(filteredProducts);
  });
  
  // Función para mostrar los productos
  function displayProducts(products) {
    var productContainer = document.getElementById('productsContainer');
    productContainer.innerHTML = ''; // Limpiar productos actuales
  
    products.forEach(function (product) {
      var productCard = document.createElement('div');
      productCard.className = 'product-card';
      productCard.innerHTML = `
        <img src="${product.image}" class="product-img" alt="${product.name}">
        <div class="product-info">
          <h3>${product.name}</h3>
          <p>${product.description}</p>
          <p class="price">S/ ${product.price}</p>
          <button class="add-to-cart">Agregar al carrito</button>
        </div>
      `;
      productContainer.appendChild(productCard);
    });
  }
  