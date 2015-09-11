var docs_toc = {
    "install": { parent: "" },

    "tutorial-cordova-5-readme": { parent: "develop" }, "cordova-5-security": { parent: "develop" }, "tutorial-gulp-readme": { parent: "develop" },
    "gulp-ci": { parent: "develop" }, "gulp-task-runner-explorer": { parent: "develop" }, "tutorial-typescript": { parent: "develop" }, "gulp-typescript": { parent: "develop" },
    "tutorial-using-bower": { parent: "develop" }, "tutorial-nuget": { parent: "develop" },

    "tutorial-team-build-readme": { parent: "debug_and_test" }, "tfs2015": { parent: "debug_and_test" }, "tfs2013": { parent: "debug_and_test" },
    "jenkins": { parent: "debug_and_test" }, "general": { parent: "debug_and_test" }, "karma": { parent: "debug_and_test" }, "chutzpah": { parent: "debug_and_test" },

    "tutorial-package-publish-readme": { parent: "package_and_publish" },

    "tips-and-workarounds-general-readme": { parent: "tips_and_workarounds" }, "tips-and-workarounds-android-readme": { parent: "tips_and_workarounds" },
    "tips-and-workarounds-ios-readme": { parent: "tips_and_workarounds" }, "tips-and-workarounds-performance-readme": { parent: "tips_and_workarounds" },

    "known-issues-general": { parent: "known_issues" }, "known-issues-debugger": { parent: "known_issues" }, "known-issues-ripple-cordova": { parent: "known_issues" },
    "known-issues-cordova5": { parent: "known_issues" }, "known-issues-android": { parent: "known_issues" }, "known-issues-ios": { parent: "known_issues" },
    "known-issues-windows": { parent: "known_issues" }, "known-issues-vs2015": { parent: "known_issues" }, "known-issues-vs2013": { parent: "known_issues" },

};

$(document).ready(function () {

    var path = window.location.pathname;
    var toc_position_set = false;

    $('.panel').each(function (i) {
        this.className = "panel collapsed";
    });

    for (var doc in docs_toc) {
        if (path.indexOf("/" + doc + "/") > -1) {
            $("#" + docs_toc[doc].parent + "_element").attr("class", "panel active expanded");
            $("#" + docs_toc[doc].parent + "_articles").addClass("in");
            $("#" + doc).addClass("active");
            $("#" + doc + "-option").attr("selected", "selected");
            toc_position_set = true;
            break;
        }
    }
    if (!toc_position_set) {
        $("#home_element").addClass("active");
        $("#home-option").addClass("selected");
    }
});