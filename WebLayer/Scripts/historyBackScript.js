$(document).ready(function () {
    window.addEventListener("popstate", function (e) {
        document.location = document.referrer;
    });
});