function AddProduct(){
    $.post( "/api/ProductNotRedis/CreateProduct",
    {
        productName: $("#productName").val(),
        productDescription: $("#productDescription").val(),
        productCost: $("#productCost").val(),
        productStock: $("#productStock").val()
    }).done(function(data) {
        console.log(data);
        AdminGetProducts();
    });
}

function UpdateProduct(product){
    $.post( "/api/ProductNotRedis/UpdateProduct", product).done(function(data) {
        console.log(data);
        AdminGetProducts();
    });
}

function DeleteProduct(id){
    $.ajax({
        url: "/api/ProductNotRedis/DeleteProduct?id=" + id,
        type: "DELETE",
        success: function(data) {
            console.log(data);
            AdminGetProducts();
        },
        error: function(data) {
            console.error(data);
        }
    });
}

function AdminGetProducts(){
    $("#productForm").children().remove();
    $.get("/api/ProductNotRedis/ProductsList")
    .done((data) =>{
        for (const iterator of data) {
        let product = $("<div></div>").addClass('card shadow').css('margin', '1rem').css('width', '18rem').css('height', '25rem');
        let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-3 shadow');
        let productName = $("<textarea></textarea>").addClass('my-1').css('height', '3rem').val(iterator['productName']);
        let productDescription = $("<textarea></textarea>").addClass('my-1').css('height', '3rem').val(`${iterator['productDescription']}`);
        let productCostStock = $("<h5></h5>").addClass('mt-auto').text(`${Math.round(iterator['productCost'] - (iterator['productCost'] / 100 * iterator['productStock']))} грн.`);
        let deleteButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg w-50 m-1').text("Delete").click(()=>{
            DeleteProduct(iterator['productId']);
        });
        let updateButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg w-50 m-1').text("Update").click(()=>{
            iterator['productName'] = productName.val();
            iterator['productDescription'] = productDescription.val();
            iterator['productStock'] = productStock.val();
            iterator['productCost'] = productCost.val();
            UpdateProduct(iterator);
        });
        let divButtons = $("<div></div>").addClass('d-flex w-100');

        let productStock = $("<input></input>").addClass('my-1').val(iterator['productStock']).change(()=>{
            productCostStock.text(`${Math.round(productCost.val() - (productCost.val() / 100 * productStock.val()))} грн.`);
        });
        let productCost = $("<input></input>").addClass('my-1').val(iterator['productCost']).change(()=>{
            productCostStock.text(`${Math.round(productCost.val() - (productCost.val() / 100 * productStock.val()))} грн.`);
        });

        let titleName = $("<small></small>").text("Name:");
        let titleDescription = $("<small></small>").text("Description:");
        let titleCost = $("<small></small>").text("Cost:");
        let titleStock = $("<small></small>").text("Stock:");
        
        productBody.append(titleName);
        productBody.append(productName);
        productBody.append(titleDescription);
        productBody.append(productDescription);
        productBody.append(titleCost);
        productBody.append(productCost);
        productBody.append(titleStock);
        productBody.append(productStock);
        productBody.append(productCostStock);
        divButtons.append(updateButton);
        divButtons.append(deleteButton);
        productBody.append(divButtons);
        product.append(productBody);
        $("#productForm").append(product);
    }
    })
    .fail(() =>{
        console.warn(data);
    });
}

function GetProducts(){
    $("#productForm").children().remove();
    $.get("/api/ProductNotRedis/ProductsList")
    .done((data) =>{
        for (const iterator of data) {
        let product = $("<div></div>").addClass('card shadow').css('margin', '1rem').css('width', '18rem').css('height', '25rem');
        let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-3 shadow');
        let productName = $("<h5></h5>").addClass('card-title w-75').css('height', '4.8rem').css('overflow', 'hidden').text(`${iterator['productName']}`);
        let divStock = $("<div></div>").addClass('position-absolute w-100 p-3');
        let productStock = $("<h6></h6>").addClass('float-end bg-secondary p-2 rounded text-white').text(`-${iterator['productStock']} %`);
        let productDescription = $("<small></small>").addClass('card-title text-secondary').css('height', '10.2rem').css('overflow', 'hidden').text(`${iterator['productDescription']}`);
        let productCost = $("<del></del>").addClass('card-text text-secondary mt-auto').text(`${iterator['productCost']} грн.`);
        let productCostStock = $("<h5></h5>").addClass('card-title').text(`${Math.round(iterator['productCost'] - (iterator['productCost'] / 100 * iterator['productStock']))} грн.`);
        let cartButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg').text("Buy");

        divStock.append(productStock);
        product.append(divStock);
        productBody.append(productName);
        productBody.append(productDescription);
        productBody.append(productCost);
        productBody.append(productCostStock);
        productBody.append(cartButton);
        product.append(productBody);
        $("#productForm").append(product);
    }
    })
    .fail(() =>{
        console.warn(data.status);
    });
}

function SignIn(){
    $("#loginForm").css('display', 'block');
    $("#productForm").css('filter', 'blur(5px)');
    $("#buttonSignIn").css('display', 'none');
}

function CloseSignIn(){
    $("#loginForm").css('display', 'none');
    $("#productForm").css('filter', 'blur(0px)');
    $("#buttonSignIn").css('display', 'block');
}

function Login(){
    $("#loginForm").css('display', 'none');
    $("#userInfo").css('display', 'block');
    $("#productForm").css('filter', 'blur(0px)');
    AdminGetProducts();
}

function Exit(){
    $("#userInfo").css('display', 'none');
    $("#buttonSignIn").css('display', 'block');
}

document.addEventListener('DOMContentLoaded', (e) => {
		var tokenKey = "accessToken";
        // при нажатии на кнопку отправки формы идет запрос к /login для получения токена
        document.getElementById("submitLogin").addEventListener("click", async e => {
            e.preventDefault();
            // отправляет запрос и получаем ответ
            const response = await fetch("/api/Authentication/login", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    userName: document.getElementById("email").value,
                    password: document.getElementById("password").value
                })
            });
            // если запрос прошел нормально
            if (response.ok === true) {
                // получаем данные
                const data = await response.json();
                // изменяем содержимое и видимость блоков на странице
                Login();
                // сохраняем в хранилище sessionStorage токен доступа
                sessionStorage.setItem(tokenKey, data.token);
            }
            else  // если произошла ошибка, получаем код статуса
                console.log("Status: ", response.status);
        });
 
        // // условный выход - просто удаляем токен и меняем видимость блоков
        document.getElementById("buttonLogOut").addEventListener("click", e => {
 
            e.preventDefault();
            Exit();
            sessionStorage.removeItem(tokenKey);
        });

        $("#buttonSignIn").click(()=>{
            SignIn();
        });

        $("#buttonLoginClose").click(()=>{
            CloseSignIn();
        });

        $("#buttonAddProduct").click(()=>{
            AddProduct();
        });

        GetProducts();

    
});