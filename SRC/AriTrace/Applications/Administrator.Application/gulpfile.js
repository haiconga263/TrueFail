/// <binding AfterBuild='copy-modules' />
// <binding AfterBuild='copy-modules' />
"use strict";

var gulp = require('gulp'),
    clean = require('gulp-clean'),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    ignore = require('gulp-ignore'),
    glob = require("glob"),
    rimraf = require("rimraf");

var listModules = [
    "MDM",
    "Users",
    "Administrator",
    "Partner",
    "GS1",
    "AriSystem",
    "Production"
];

var listInterface = [
    "MDM.UI",
    "Users.UI",
    "Administrator.UI",
    "Partner.UI",
    "GS1.UI",
    "AriSystem.UI",
    "Production.UI"
];

var listExtensions = [
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
    gulp.src("./Views/", { read: false })
        .pipe(clean());

    for (var i = 0; i < listModules.length; i++) {
        gulp.src(paths.destination.moduleRun + listModules[i] + ".*", { read: false })
            .pipe(clean());
    }
    for (var j = 0; j < listInterface.length; j++) {
        gulp.src(paths.destination.moduleRun + listInterface[j] + ".*", { read: false })
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
            .pipe(gulp.dest("./Modules/" + listModules[i] + "/Views/", { overwrite: true }));
        gulp.src(paths.source.modules + listModules[i] + '/wwwroot/**/*.*')
            .pipe(gulp.dest(paths.destination.wwwroot, { overwrite: true }));
    }
});

gulp.task('copy-modules', [], function () {
    //gulp.start(['clean-module']);
    gulp.start(['copy-static']);

    for (var i = 0; i < listModules.length; i++) {
        console.log(paths.destination.modules + listModules[i] + paths.destination.moduleBin);
        console.log(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*');
        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
            .pipe(gulp.dest(paths.destination.modules + listModules[i] + paths.destination.moduleBin, { overwrite: true }));
        //gulp.src(paths.source.modules + listModules[i] + '/ClientApp/app/components/**/*.*')
        //    .pipe(gulp.dest('./ClientApp/app/components/'));

        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listModules[i] + '.*')
            .pipe(gulp.dest(paths.destination.moduleRun, { overwrite: true }));
    }

    for (var j = 0; j < listInterface.length; j++) {
        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
            .pipe(gulp.dest(paths.destination.modules + listInterface[j] + paths.destination.moduleBin, { overwrite: true }));
        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listModules[i] + '.*')
            .pipe(gulp.dest(paths.destination.moduleRun, { overwrite: true }));
    }

    for (var t = 0; t < listExtensions.length; t++) {
        console.log(paths.source.extensions + listExtensions[t] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listExtensions[t] + '.dll');
        console.log(paths.destination.extension, { overwrite: true });
        gulp.src(paths.source.extensions + listExtensions[t] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listExtensions[t] + '.dll')
            .pipe(gulp.dest(paths.destination.extension, { overwrite: true }));
    }

    gulp.src('../' + paths.source.modules + 'Users' + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
        .pipe(gulp.dest(paths.destination.modules + 'Users' + paths.destination.moduleBin, { overwrite: true }));
    gulp.src('../' + paths.source.modules + 'Users' + paths.source.moduleBin + 'Debug/netcoreapp*/' + 'Users' + '.*')
        .pipe(gulp.dest(paths.destination.moduleRun, { overwrite: true }));

    gulp.src('../' + paths.source.interfaces + 'Users.UI' + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
        .pipe(gulp.dest(paths.destination.modules + 'Users.UI' + paths.destination.moduleBin, { overwrite: true }));
    gulp.src('../' + paths.source.interfaces + 'Users.UI' + paths.source.moduleBin + 'Debug/netcoreapp*/' + 'Users' + '.*')
        .pipe(gulp.dest(paths.destination.moduleRun, { overwrite: true }));

});