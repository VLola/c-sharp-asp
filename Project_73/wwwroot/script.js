async function GetProducts(){

    const response = await fetch("/api/ProductNotRedis/ProductsList", {
        method: "GET"
    });

    if (response.ok === true) {
        const data = await response.json();

        for (const iterator of data) {
            let product = $("<div></div>").addClass('card shadow').css('margin', '1rem').css('width', '18rem').css('height', '25rem');
            let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-3 shadow');
            let productName = $("<h5></h5>").addClass('card-title w-75').css('height', '4.8rem').css('overflow', 'hidden').text(`${iterator['productName']}`);
            let divStock = $("<div></div>").addClass('position-absolute w-100 p-3');
            let productStock = $("<h5></h5>").addClass('float-end bg-secondary p-2 rounded text-white').text(`-${iterator['productStock']} %`);
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
            
    }
    else console.log("Status: ", response.status);
}

function SignIn(){
    $("#buttonSignIn").css('display', 'none');
    $("#loginForm").css('display', 'block');
    $("#productForm").css('filter', 'blur(5px)');
}

function CloseSignIn(){
    $("#buttonSignIn").css('display', 'block');
    $("#loginForm").css('display', 'none');
    $("#productForm").css('filter', 'blur(0px)');
}

function Login(){
    $("#loginForm").css('display', 'none');
    $("#userInfo").css('display', 'block');
    $("#buttonLogOut").css('display', 'block');
    $("#productForm").css('filter', 'blur(0px)');
}

function Exit(){
    $("#userInfo").css('display', 'none');
    $("#buttonLogOut").css('display', 'none');
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
 
        // // кнопка для обращения по пути "/data" для получения данных
        // document.getElementById("getData").addEventListener("click", async e => {
        //     e.preventDefault();
        //     // получаем токен из sessionStorage
        //     const token = sessionStorage.getItem(tokenKey);
        //     // отправляем запрос к "/data
        //     const response = await fetch("/api/ProductNotRedis/ProductsList", {
        //         method: "GET",
        //         headers: {
        //             "Accept": "application/json",
        //             "Authorization": "Bearer " + token  // передача токена в заголовке
        //         }
        //     });
 
        //     if (response.ok === true) {
        //         const data = await response.json();
        //         console.log(data);
        //     }
        //     else console.log("Status: ", response.status);
        // });
 
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

        GetProducts();

    
});