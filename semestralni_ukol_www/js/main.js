function clocks() {
    var dateNow = new Date();

    var day = dateNow.getDay();
    var dayM = dateNow.getDate();
    var month = dateNow.getMonth() + 1;
    var year = dateNow.getFullYear();
    var dayArray = new Array("Neděle", "Pondělí", "Úterý", "Středa", "Čtvrtek", "Pátek", "Sobota");

    var h = dateNow.getHours();
    if (h == 24) h = 0;
    if (h < 10) h = "0" + h;

    var m = dateNow.getMinutes();
    if (m < 10) m = "0" + m;

    var s = dateNow.getSeconds();
    if (s < 10) s = "0" + s;

    var myClock = document.getElementById("timer");
    myClock.textContent = dayArray[day] + " " + dayM + "." + month + "." + year + " | " + h + ":" + m + ":" + s;

    var dateDayOne = document.getElementById("futureBoxA");
    var dateDayTwo = document.getElementById("futureBoxB");
    var dateDayThree = document.getElementById("futureBoxC");
    var dateDayFour = document.getElementById("futureBoxD");
    var dateDayFive = document.getElementById("futureBoxE");
    var dateDaySix = document.getElementById("futureBoxF");

    arrayOfFutureDays = new Array(dateDayOne, dateDayTwo, dateDayThree, dateDayFour, dateDayFive, dateDaySix);

    for (let i = 1; i < arrayOfFutureDays.length + 1; i++) {
        const element = arrayOfFutureDays[i - 1];
        var dayF = dateNow.getDay() + i;
        var monthF = dateNow.getMonth() + 1;
        var dayMF = dateNow.getDate() + i;
        var futureDay = day + i;
        if (futureDay % 7 == 0) futureDay = 0;
        if (futureDay % 7 == 1) futureDay = 1;
        if (futureDay % 7 == 2) futureDay = 2;
        if (futureDay % 7 == 3) futureDay = 3;
        if (futureDay % 7 == 4) futureDay = 4;
        if (futureDay % 7 == 5) futureDay = 5;
        if (futureDay % 7 == 6) futureDay = 6;

        if (dayF < day) monthF = dateNow.getMonth() + 2;

        element.textContent = dayArray[futureDay] + " " + dayMF + "." + monthF + ".";
    }
    setTimeout("clocks()", 1000);
}



var userInput = document.querySelector('#userInput');
var buttonSearch = document.querySelector('#buttonSearch');
var tmpNow = document.querySelector('#tempToday');
var hmNow = document.querySelector('#humToday');
var head = document.querySelector('#weatherTodayHeader');
var imgDesc;
var mainImage = document.querySelector('#mainImage');
var currentCity = "";


buttonSearch.onclick = function () {
    if (userInput.value == "") {
        alert("Textové pole nesmí být prázdné!");
        return false;
    }
    else {
        loadWeatherInCity();
    }
}

function loadWeatherInCity() {
    var cityToSearch;
    if (userInput.value == "") {
        cityToSearch = "Praha";

    }
    else cityToSearch = userInput.value;
    console.log(cityToSearch);
    head.textContent = "Počasí dnes ve městě" + " " + cityToSearch;
    currentCity = cityToSearch;

    var url = 'http://api.weatherstack.com/current?access_key=a46d371d899330453c946fbc22c66a34&unit=m&query=' + cityToSearch;


    fetch(url)
        .then(response => {
            /*if (response.success != true) {
                console.log('invalid input');
                userInput.value == "";
                break;
            }
            else*/ return response.json()
        })
        .then(data => {
            console.log(data.current.temperature);
            currentCity = cityToSearch;
            tmpNow.textContent = data.current.temperature + " " + "°C";
            hmNow.textContent = "Vlhkost" + " " + data.current.humidity + "%";
            imgDesc = data.current.weather_descriptions;
            mainImage.src = data.current.weather_icons;
            mainImage.alt = imgDesc;
            /* insertImage();*/
            console.log('success');
        })
}

/*function insertImage() {
    if (imgDesc == "Partly cloudy") {
        mainImage.src = "images/day_partial_cloud.png"
    }
    else if (imgDesc == "Sunny") {
        mainImage.src = "images/day_clear.png"
    }
}*/


var buttonSave = document.querySelector('#buttonSaveFavourite');
var listOfElements = document.querySelector('#favouriteList');


buttonSave.onclick = function () {
    if (currentCity != "") {
        var valid = ignoreDuplicity();
        if (valid == 1) {
            localStorage.setItem(`${listOfElements.childElementCount}`, currentCity);
            const newElement = document.createElement("OPTION");
            newElement.innerText = currentCity;
            newElement.value = currentCity;
            listOfElements.appendChild(newElement);
            alert("Město přidáno do oblíbených");
        }
    }
}

function ignoreDuplicity() {
    if (localStorage != 0) {
        for (let i = 0; i < localStorage.length; i++) {
            if (currentCity == localStorage.getItem(i)) return 0;
        }
    }
    return 1;
}

function loadFavourites() {
    if (localStorage != 0) {
        for (let i = 0; i < localStorage.length; i++) {
            const newElement = document.createElement("OPTION");
            newElement.innerText = localStorage.getItem(i);
            newElement.value = localStorage.getItem(i);
            listOfElements.appendChild(newElement);
        }
    }
}

var favButton = document.querySelector('#favButton');

function showSelected() {
    var selectedFav = document.getElementById("favouriteList").value;
    if (selectedFav == "Oblíbená města") return;
    else {
        userInput.value = selectedFav;
        loadWeatherInCity();
    }
}


