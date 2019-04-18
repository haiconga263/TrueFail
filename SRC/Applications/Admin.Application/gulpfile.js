/// <binding AfterBuild='copy-modules' />
// <binding AfterBuild='copy-modules' />
"use strict";

var gulp = require('gulp'),
    clean = require('gulp-clean'),
    flatten = require('gulp-flatten'),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    ignore = require('gulp-ignore'),
    glob = require("glob"),
    rimraf = require("rimraf");

var listModules = [
    "Admin",
    "MDM",
    "Inventory",

    "Farmers",
    "Users",
    "Order",
    "Collections",
    "Distributions",
    "Retailers",
    "Farmers",
    "Fulfillments",
    "Productions",
];

var listInterface = [
    "Admin.UI",
    "MDM.UI",
    "Inventory.UI",
    "Users.UI",

    "Farmers.UI",
    "Users.UI",
    "Order.UI",
    "Collections.UI",
    "Distributions.UI",
    "Retailers.UI",
    "Fulfillments.UI",
    "Productions.UI",
];

var listExtensions = [
    "Admin.Extensions",
];

var paths = {
    source: {
        wwwroot: "./wwwroot/",
        moduleBin: "/bin/",
        modules: "../../Modules/",
        interfaces: "../../Presentations/",
        extensions: "../../Extensions/"
    },
    destination: {
        wwwroot: "./wwwroot/",
        modules: "./Modules/",
        extension: "./ExtensionFolder/",
        moduleBin: "/bin/",
        moduleRun: "./bin/Debug/netcoreapp2.0/"
    }
};

gulp.task('clean-module', function () {
    //gulp.src("./Views/", { read: false })
    //    .pipe(clean());

    for (var i = 0; i < listModules.length; i++) {
        gulp.src(paths.destination.moduleRun + listModules[i] + ".dll", { read: false })
            .pipe(clean());
    }
    for (var j = 0; j < listInterface.length; j++) {
        gulp.src(paths.destination.moduleRun + listInterface[j] + ".dll", { read: false })
            .pipe(clean());
    }

    gulp.src("./ExtensionFolder/", { read: false })
        .pipe(clean());

    return gulp.src(paths.destination.modules + '*', { read: false })
        .pipe(clean());
});

gulp.task('copy-static', function () {

    for (var i = 0; i < listModules.length; i++) {
        gulp.src(paths.source.modules + listModules[i] + '/Views/**/*.*')
            .pipe(gulp.dest("./Modules/" + listModules[i] + "/Views/"));
        gulp.src(paths.source.modules + listModules[i] + '/wwwroot/**/*.*')
            .pipe(gulp.dest(paths.destination.wwwroot));
    }
});

gulp.task('copy-modules', ['clean-module'], function () {
    gulp.start(['copy-static']);

    for (var i = 0; i < listModules.length; i++) {
        console.log("module: " + listModules[i]);
        console.log("____ " + paths.source.modules + listModules[i] + paths.source.moduleBin);

        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
            .pipe(gulp.dest(paths.destination.modules + listModules[i] + paths.destination.moduleBin));
        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listModules[i] + '.*')
            .pipe(gulp.dest(paths.destination.moduleRun));
    }

    for (var j = 0; j < listInterface.length; j++) {
        console.log("interface: " + listInterface[j]);
        console.log("____ " + paths.source.modules + listInterface[j] + paths.source.moduleBin);

        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
            .pipe(gulp.dest(paths.destination.modules + listInterface[j] + paths.destination.moduleBin));
        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listModules[i] + '.*')
            .pipe(gulp.dest(paths.destination.moduleRun));
    }

    for (var t = 0; t < listExtensions.length; t++) {
        console.log("extension: " + listExtensions[t]);
        console.log("____ " + paths.source.modules + listExtensions[t] + paths.source.moduleBin);

        gulp.src(paths.source.extensions + listExtensions[t] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listExtensions[t] + '.dll')
            .pipe(flatten())
            .pipe(gulp.dest(paths.destination.extension));
    }
});
