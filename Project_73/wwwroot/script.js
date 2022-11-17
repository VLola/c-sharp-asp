function AddProduct(){
    $.post( "/api/ProductNotRedis/CreateProduct",
    {
        productName: $("#productName").val(),
        productDescription: $("#productDescription").val(),
        productCost: $("#productCost").val(),
        productStock: $("#productStock").val(),
        productDiscount: $("#productDiscount").val(),
        productImageUrl: $("#productImageUrl").val(),
    }).done(function(data) {
        console.log(data);
        AdminGetProducts();
    });
}

async function UpdateProduct(product){
    await $.post( "/api/ProductNotRedis/UpdateProduct", product).done(async function(data) {
        await console.log(data);
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
        let product = $("<div></div>").addClass('card shadow').css('margin', '1rem').css('width', '18rem').css('height', '31rem');
        let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-3 shadow');
        let productName = $("<textarea></textarea>").addClass('mt-1').css('height', '3rem').val(iterator['productName']);
        let productDescription = $("<textarea></textarea>").addClass('mt-1').css('height', '3rem').val(`${iterator['productDescription']}`);
        let productPrice = $("<h5></h5>").addClass('mt-auto').text(`${Math.round(iterator['productCost'] - (iterator['productCost'] / 100 * iterator['productDiscount']))} грн.`);
        let deleteButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg w-50 m-1').text("Delete").click(()=>{
            DeleteProduct(iterator['productId']);
        });
        
        let productDiscount = $("<input></input>").addClass('mt-1').val(iterator['productDiscount']).change(()=>{
            productPrice.text(`${Math.round(productCost.val() - (productCost.val() / 100 * productDiscount.val()))} грн.`);
        });

        let productCost = $("<input></input>").addClass('mt-1').val(iterator['productCost']).change(()=>{
            productPrice.text(`${Math.round(productCost.val() - (productCost.val() / 100 * productDiscount.val()))} грн.`);
        });

        let productStock = $("<input></input>").addClass('mt-1').val(iterator['productStock']);
        let productImageUrl = $("<input></input>").addClass('mt-1').val(iterator['productImageUrl']);

        let divButtons = $("<div></div>").addClass('d-flex w-100');
        let updateButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg w-50 m-1').text("Update").click(async ()=>{
            iterator['productName'] = productName.val();
            iterator['productDescription'] = productDescription.val();
            iterator['productDiscount'] = productDiscount.val();
            iterator['productCost'] = productCost.val();
            iterator['productStock'] = productStock.val();
            iterator['productImageUrl'] = productImageUrl.val();
            await UpdateProduct(iterator);
            AdminGetProducts();
        });

        let titleName = $("<small></small>").text("Name:");
        let titleDescription = $("<small></small>").text("Description:");
        let titleCost = $("<small></small>").text("Cost:");
        let titleDiscount = $("<small></small>").text("Discount:");
        let titleStock = $("<small></small>").text("Stock:");
        let titleImageUrl = $("<small></small>").text("Image url:");
        
        productBody.append(titleName);
        productBody.append(productName);
        productBody.append(titleDescription);
        productBody.append(productDescription);
        productBody.append(titleCost);
        productBody.append(productCost);
        productBody.append(titleDiscount);
        productBody.append(productDiscount);
        productBody.append(titleStock);
        productBody.append(productStock);
        productBody.append(titleImageUrl);
        productBody.append(productImageUrl);
        productBody.append(productPrice);
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
    .done(async (data) =>{
        for (const iterator of data) {
        let product = $("<div></div>").addClass('card shadow').css('margin', '1rem').css('width', '18rem').css('height', '25rem');
        let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-3 shadow');
        let productName = $("<h5></h5>").addClass('card-title w-75').css('height', '4.8rem').css('overflow', 'hidden').text(`${iterator['productName']}`);
        let divDiscount = $("<div></div>").addClass('position-absolute w-100 p-3');
        let productDiscount = $("<h6></h6>").addClass('float-end bg-secondary p-2 rounded text-white').text(`-${iterator['productDiscount']} %`);

        
        let productImage = $("<img></img>").addClass('h-100 mx-auto').css('display', 'block').attr("src", iterator['productImageUrl']);
        let productDescription = $("<small></small>").addClass('card-title text-secondary').css('display', 'none').text(`${iterator['productDescription']}`);
        let divImageAndDescription = $("<div></div>").addClass('w-100').css('height', '10.2rem').css('overflow', 'hidden').mouseenter(function() {
            productImage.css('display', 'none');
            productDescription.css('display', 'block');
          })
          .mouseleave(function() {
            productDescription.css('display', 'none');
            productImage.css('display', 'block');
          });
        divImageAndDescription.append(productImage);
        divImageAndDescription.append(productDescription);

        
        
        let productCost = $("<del></del>").addClass('card-text text-secondary mt-auto').text(`${iterator['productCost']} UAH.`);
        let productPrice = $("<h5></h5>").addClass('card-title').text(`${Math.round(iterator['productCost'] - (iterator['productCost'] / 100 * iterator['productDiscount']))} UAH.`);
        let productButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg').text("Buy").click(async function() {
            if(iterator['productStock'] > 0){
                iterator['productStock'] = iterator['productStock'] - 1;
                await UpdateProduct(iterator);
                GetProducts();
            }
        });

        if(iterator['productStock'] <= 0){
            productButton.prop('disabled', true);
            productPrice.text('Not available');
        }
        divDiscount.append(productDiscount);
        product.append(divDiscount);
        productBody.append(productName);
        productBody.append(divImageAndDescription);
        productBody.append(productCost);
        productBody.append(productPrice);
        productBody.append(productButton);
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
            GetProducts();
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