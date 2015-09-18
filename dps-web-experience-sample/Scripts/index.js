var docs_toc = {
    "install": { parent: "" },

    "get-started-vs-tools-apache-cordova": { parent: "get_started" }, "install-vs-tools-apache-cordova": { parent: "get_started" },
    "configure-vs-tools-apache-cordova": { parent: "get_started" }, "create-first-app-using-vs-tools-apache-cordova": { parent: "get_started" },
    "migrate-to-vs2015": { parent: "get_started" },

    "tutorial-cordova-5-readme": { parent: "develop" }, "cordova-5-security": { parent: "develop" }, "configure-app": { parent: "develop" },
    "manage-plugins": { parent: "develop" }, "createplugintutorial": { parent: "develop" }, "create-plugin-windows-phone": { parent: "develop" },
    "add-connected-services": { parent: "develop" }, "access-native-cordova-project": { parent: "develop" }, "tutorial-gulp-readme": { parent: "develop" },
    "gulp-ci": { parent: "develop" }, "gulp-task-runner-explorer": { parent: "develop" }, "tutorial-typescript": { parent: "develop" },
    "gulp-typescript": { parent: "develop" }, "tutorial-using-bower": { parent: "develop" }, "tutorial-nuget": { parent: "develop" }, "interoperability": { parent: "develop" },


    "deploy-and-run-app": { parent: "debug_and_test" }, "run-app-ripple-simulator": { parent: "debug_and_test" }, "run-app-apache": { parent: "debug_and_test" },
    "run-app-ios": { parent: "debug_and_test" }, "run-app-windows": { parent: "debug_and_test" }, "run-app-windows-phone": { parent: "debug_and_test" },
    "run-android-emulator-on-osx-using-parallels": { parent: "debug_and_test" }, "build_ios_cloud": { parent: "debug_and_test" }, "build_ios_parallels": { parent: "debug_and_test" },
    "debug-using-visual-studio": { parent: "debug_and_test" }, "debug-using-vs-diagnostic-tools": { parent: "debug_and_test" }, "alm-cordova": { parent: "debug_and_test" },
    "tutorial-team-build-readme": { parent: "debug_and_test" }, "general": { parent: "debug_and_test" }, "tfs2015": { parent: "debug_and_test" },
    "tfs2013": { parent: "debug_and_test" }, "jenkins": { parent: "debug_and_test" }, "karma": { parent: "debug_and_test" },
    "chutzpah": { parent: "debug_and_test" },

    "package-app-built-with-visual-studio": { parent: "package_and_publish" }, "tutorial-package-publish-readme": { parent: "package_and_publish" },
    "get-your-app-approved": { parent: "package_and_publish" }, "publish-app-built-with-visual-studio": { parent: "package_and_publish" },

    "create-to-do-list-app": { parent: "tutorials_and_samples" }, "tutorial-ionic": { parent: "tutorials_and_samples" }, "o365-files": { parent: "tutorials_and_samples" }, "o365-ionic": { parent: "tutorials_and_samples" },
    "convert_cordova_phonegap": { parent: "tutorials_and_samples" },

    "tips-and-workarounds-general-readme": { parent: "tips_and_workarounds" }, "tips-and-workarounds-android-readme": { parent: "tips_and_workarounds" },
    "tips-and-workarounds-ios-readme": { parent: "tips_and_workarounds" }, "tips-and-workarounds-performance-readme": { parent: "tips_and_workarounds" },
    "tips-and-workarounds-ios-ios-plugin-symlink-fix-readme": { parent: "tips_and_workarounds" }, "tips-and-workarounds-ios-osx-set-execute-readme": { parent: "tips_and_workarounds" },

    "known-issues-general": { parent: "known_issues" }, "known-issues-debugger": { parent: "known_issues" }, "known-issues-ripple-cordova": { parent: "known_issues" },
    "known-issues-cordova5": { parent: "known_issues" }, "known-issues-android": { parent: "known_issues" }, "known-issues-ios": { parent: "known_issues" },
    "known-issues-windows": { parent: "known_issues" }, "known-issues-vs2015": { parent: "known_issues" },
    "known-issues-vs2013": { parent: "known_issues" }, "known-issues-npm": { parent: "known_issues" }, "known-issues-plugin-install": { parent: "known_issues" },

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
