var min = 0;
var max = 100000;
var category = "skif";
var page = 1;
var isLogin = false;
async function AddProduct(){
    const token = sessionStorage.getItem(tokenKey);
    await $.ajax({
        url: '/api/Knife/CreateKnife',
        type: 'POST',
        data: {
            id: 0,
            name: $("#productName").val(),
            description: $("#productDescription").val(),
            cost: $("#productCost").val(),
            stock: $("#productStock").val(),
            imgUrl: $("#productImageUrl").val(),
            steelHardness: $("#productSteelHardness").val(),
            steelGrade: $("#productSteelGrade").val(),
            liningMaterial: $("#productLiningMaterial").val(),
        },
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        },
        success: function (data){
            console.log(data);
            AdminGetProducts();
        },
        error: function (data){
            console.error(data);  
        }
    });
}

async function UpdateProduct(product){
    await $.post( "/api/Knife/UpdateKnife", product)
    .done(function(data) {
        console.log(data);
    });
}

function DeleteProduct(id){
    const token = sessionStorage.getItem(tokenKey);
    $.ajax({
        url: "/api/Knife/DeleteKnife?id=" + id,
        type: "DELETE",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        },
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
    $.get(`/api/Knife/KnifesList?page=${page}&category=${category}&min=${min}&max=${max}`)
    .done((data) =>{
        for (const iterator of data) {
        let product = $("<div></div>").addClass('card shadow').css('margin', '1rem').css('width', '18rem').css('height', '36rem');
        let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-3 shadow');
        let productName = $("<textarea></textarea>").addClass('mt-1').css('height', '3rem').val(iterator['name']);
        let productDescription = $("<textarea></textarea>").addClass('mt-1').css('height', '3rem').val(`${iterator['description']}`);
        let deleteButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg w-50 m-1').text("Delete").click(()=>{
            DeleteProduct(iterator['id']);
        });
        
        let productCost = $("<input></input>").addClass('mt-1').val(iterator['cost']);
        let productStock = $("<input></input>").addClass('mt-1').val(iterator['stock']);
        let productImageUrl = $("<input></input>").addClass('mt-1').val(iterator['imgUrl']);
        let productSteelHardness = $("<input></input>").addClass('mt-1').val(iterator['steelHardness']);
        let productSteelGrade = $("<input></input>").addClass('mt-1').val(iterator['steelGrade']);
        let productLiningMaterial = $("<input></input>").addClass('mt-1').val(iterator['liningMaterial']);

        let divButtons = $("<div></div>").addClass('d-flex w-100');
        let updateButton = $("<button></button>").addClass('btn btn-outline-dark btn-lg w-50 m-1').text("Update").click(async ()=>{
            iterator['name'] = productName.val();
            iterator['description'] = productDescription.val();
            iterator['cost'] = productCost.val();
            iterator['stock'] = productStock.val();
            iterator['imgUrl'] = productImageUrl.val();
            iterator['steelHardness'] = productSteelHardness.val();
            iterator['steelGrade'] = productSteelGrade.val();
            iterator['liningMaterial'] = productLiningMaterial.val();
            await UpdateProduct(iterator);
            AdminGetProducts();
        });

        let titleName = $("<small></small>").text("Name:");
        let titleDescription = $("<small></small>").text("Description:");
        let titleCost = $("<small></small>").text("Cost:");
        let titleStock = $("<small></small>").text("Stock:");
        let titleImageUrl = $("<small></small>").text("Image url:");
        let titleSteelHardness = $("<small></small>").text("Steel Hardness:");
        let titleSteelGrade = $("<small></small>").text("Steel Grade:");
        let titleLiningMaterial = $("<small></small>").text("Lining Material:");
        
        productBody.append(titleName);
        productBody.append(productName);
        productBody.append(titleDescription);
        productBody.append(productDescription);
        productBody.append(titleCost);
        productBody.append(productCost);
        productBody.append(titleStock);
        productBody.append(productStock);
        productBody.append(titleImageUrl);
        productBody.append(productImageUrl);
        productBody.append(titleSteelHardness);
        productBody.append(productSteelHardness);
        productBody.append(titleSteelGrade);
        productBody.append(productSteelGrade);
        productBody.append(titleLiningMaterial);
        productBody.append(productLiningMaterial);
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
    $.get(`/api/Knife/KnifesList?page=${page}&category=${category}&min=${min}&max=${max}`)
    .done(async (data) =>{
        for (const iterator of data) {
        let product = $("<div></div>").addClass('card').css('padding', '0rem').css('margin', '1rem').css('width', '18rem').css('height', '32rem')
        .mouseenter(function() {
            product.addClass("shadow");
            product.css( { transition: "transform 0.2s", transform:  "scale(1.05, 1.05)" } );
          })
        .mouseleave(function() {
            product.removeClass("shadow");
            product.css( { transition: "transform 0.1s", transform:  "scale(1.0, 1.0)" } );
          });
        let productBody = $("<div></div>").addClass('card-body d-flex flex-column p-4');
        let productName = $("<h5></h5>").css('cursor','pointer').css('font-family',"'oswald',sans-serif").text(`${iterator['name'].toLocaleUpperCase()}`);

        let productImage = $("<img></img>").addClass('h-100 mx-auto').css('display', 'block').attr("src", iterator['imgUrl']);
        let divImageAndDescription = $("<div></div>").addClass('w-100').css('height', '13rem').css('cursor', 'pointer');
        
        divImageAndDescription.append(productImage);
        
        let productSteelHardness = $("<li></li>").css('font-family',"'oswald',sans-serif").css('font-size', '14px').text(iterator['steelHardness']);
        let productSteelGrade = $("<li></li>").css('font-family',"'oswald',sans-serif").css('font-size', '14px').text(iterator['steelGrade']);
        let productLiningMaterial = $("<li></li>").css('font-family',"'oswald',sans-serif").css('font-size', '14px').text(iterator['liningMaterial']);
        
        let productCost = $("<h5></h5>").addClass('mt-auto').css('font-family',"'oswald',sans-serif").text(`${iterator['cost']} грн.`);
        
        let productButton = $("<button></button>").addClass('button__style py-2').text("КУПИТЬ").click(async function() {
            if(iterator['stock'] > 0){
                iterator['stock'] = iterator['stock'] - 1;
                await UpdateProduct(iterator);
                GetProducts();
            }
        });

        productBody.append(divImageAndDescription);
        productBody.append(productName);
        if(iterator['stock'] > 0){
            let productAvailability = $("<small></small>").addClass('text-success mb-3').text("В наличии");
            productBody.append(productAvailability);
        }
        if(iterator['steelHardness'] != "no"){
            productBody.append(productSteelHardness);
        }
        if(iterator['steelGrade'] != "no"){
            productBody.append(productSteelGrade);
        }
        if(iterator['liningMaterial'] != "no"){
            productBody.append(productLiningMaterial);
        }
        productBody.append(productCost);
        if(iterator['stock'] <= 0){
            productCost.css('color', 'gray');
        }
        else{
            productBody.append(productButton);
        }
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
    $("#topHeader").css('filter', 'blur(5px)');
    $("#buttonSignIn").css('display', 'none');
}

function CloseSignIn(){
    $("#loginForm").css('display', 'none');
    $("#productForm").css('filter', 'blur(0px)');
    $("#topHeader").css('filter', 'blur(0px)');
    $("#buttonSignIn").css('display', 'block');
}

function Login(){
    isLogin = true;
    $("#loginForm").css('display', 'none');
    $("#userInfo").css('display', 'block');
    $("#productForm").css('filter', 'blur(0px)');
    $("#topHeader").css('filter', 'blur(0px)');
    AdminGetProducts();
}

function Exit(){
    isLogin = false;
    $("#userInfo").css('display', 'none');
    $("#buttonSignIn").css('display', 'block');
}


function SelectCheckBox(){
    let check = $("#typeCheckBox:checked").is(":checked");
    if(check !== true){
        $("#textCheckBox").text("Don't have an account?");
        $("#submitLogin").text("Login");
    }
    else{
        $("#textCheckBox").text("Do you have an account?");
        $("#submitLogin").text("Register");
    }
}

var tokenKey = "accessToken";

function Autorisation(){
    let password = $("#password").val();
    if(Validate(password)){
        $.post("/api/Authentication/login", {
            email: $("#email").val(),
            password: password,
        })
        .done(function(response) {
            Login();
            sessionStorage.setItem(tokenKey, response.token);
        })
        .fail((response) =>{
            alert(response.status);
        });
    }
}

function Registration(){
    let password = $("#password").val();
    if(Validate(password)){
        $.post("/api/Authentication/registration", {
            email: $("#email").val(),
            password: password,
        })
        .done(function(response) {
            console.log(response);
            alert("Registration successful!");
        })
        .fail((response) =>{
            alert(response.status);
        });
    }
}

function Validate(password){
    if(ValidateEmail()){
        if(ValidatePassword(password)){
            return true;
        }
        else{
            alert("Password incorrect!");
            return false;
        }
    }
    else {
        alert("Email incorrect!");
        return false;
    }
}

function ValidateEmail() {
    return $("#email").is(':valid');
}


function ValidatePassword(password) {
    return password.length > 7;
}

function GetPages() {
    $("#pages").children().remove();
    $.get(`/api/Knife/GetPages?category=${category}&min=${min}&max=${max}`)
    .done((data) =>{

        let liPrevious = $("<li></li>").addClass('page-item');
        let aPrevious = $("<li></li>").addClass('page-link text-dark').css('cursor', 'pointer').text("Previous").click(()=>{
            if(page > 1) {
                page = page - 1;
                GetPages();
                if(isLogin)AdminGetProducts();
                else GetProducts();
            }
        });
        liPrevious.append(aPrevious);
        $("#pages").append(liPrevious);

        for(let i = 1; i <= data; i++){
            let li = $("<li></li>").addClass('page-item');
            if(i == page) li.addClass('bg-secondary');
            let a = $("<li></li>").addClass('page-link text-dark').css('cursor', 'pointer').text(i).click(()=>{
                if(page != i) {
                    page = i;
                    GetPages();
                    if(isLogin)AdminGetProducts();
                    else GetProducts();
                }
            });
            if(i == page) a.addClass('bg-warning');
            li.append(a);
            $("#pages").append(li);
        }

        let liNext = $("<li></li>").addClass('page-item');
        let aNext = $("<li></li>").addClass('page-link text-dark').css('cursor', 'pointer').text("Next").click(()=>{
            if(page < data){
                page = page + 1;
                GetPages();
                if(isLogin)AdminGetProducts();
                else GetProducts();
            }
        });
        liNext.append(aNext);
        $("#pages").append(liNext);
    })
    .fail(() =>{
        console.warn(data);
    });
}

document.addEventListener('DOMContentLoaded', (e) => {

        $("#typeCheckBox").change(()=>{
            SelectCheckBox();
        });
        
        $("#submitLogin").click(()=>{
            let check = $("#typeCheckBox:checked").is(":checked");
            if(check !== true){
                Autorisation();
            }
            else{
                Registration();
            }
        });
        
        $("#buttonLogOut").click(()=>{
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

        $("#button__filter").click(()=>{
            let minPrice = $("#input__min-price").val();
            let maxPrice = $("#input__max-price").val();

            if(minPrice > 0) min = minPrice;
            else min = 0;

            if(maxPrice > 0 && maxPrice < 100000) max = maxPrice;
            else max = 100000;

            page = 1;
            GetPages();
            if(isLogin)AdminGetProducts();
            else GetProducts();
        });

        $("#category-all").click(()=>{
            category = "skif";
            page = 1;
            GetPages();
            if(isLogin)AdminGetProducts();
            else GetProducts();
        });

        $("#category-knife").click(()=>{
            category = "нож";
            page = 1;
            GetPages();
            if(isLogin)AdminGetProducts();
            else GetProducts();
        });

        $("#category-machete").click(()=>{
            category = "мачете";
            page = 1;
            GetPages();
            if(isLogin)AdminGetProducts();
            else GetProducts();
        });

        GetPages();
        GetProducts();

});