(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
ga('create', 'UA-66818411-1', 'auto');

// strip off trailing slashes
var pathnameWithoutTrailingSlash = window.location.pathname.toLowerCase();
var pathnameLength = pathnameWithoutTrailingSlash.length;

if (pathnameLength > 1 && pathnameWithoutTrailingSlash.lastIndexOf("/") == pathnameLength - 1) {
    pathnameWithoutTrailingSlash = pathnameWithoutTrailingSlash.substring(0, pathnameLength - 1);
}
ga("send", "pageview", pathnameWithoutTrailingSlash);

window.onhashchange = function () {
    ga("send", "pageview", window.location.pathname.toLowerCase() + window.location.hash);
};