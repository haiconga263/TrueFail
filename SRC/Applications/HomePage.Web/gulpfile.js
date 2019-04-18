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
  "Homepage",
  "MDM",
  "Admin",
  "Inventory",

  "Users",
  "Order",
  "Collections",
  "Distributions",
  "Retailers",
  "Farmers",
  "Fulfillments"
];

var listInterface = [
  "Homepage.UI",
  "MDM.UI",
  "Admin.UI",
  "Inventory.UI",
  "Users.UI",
  "Order.UI",
  "Collections.UI",
  "Distributions.UI",
  "Retailers.UI",
  "Farmers.UI",
  "Fulfillments.UI"
];

var listExtensions = [
];

var paths = {
  source: {
    wwwroot: "./wwwroot/",
    moduleBin: "/bin/",
    modules: "../../Modules/",
    interfaces: "../../Presentations/",
    extensions: "../Extensions/"
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
    console.log(paths.destination.modules + listModules[i] + paths.destination.moduleBin);
    console.log(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*');
    gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
      .pipe(gulp.dest(paths.destination.modules + listModules[i] + paths.destination.moduleBin));
    //gulp.src(paths.source.modules + listModules[i] + '/ClientApp/app/components/**/*.*')
    //    .pipe(gulp.dest('./ClientApp/app/components/'));

    gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listModules[i] + '.*')
      .pipe(gulp.dest(paths.destination.moduleRun));
  }
  for (var j = 0; j < listInterface.length; j++) {
    gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + 'Debug/netcoreapp*/*.*')
      .pipe(gulp.dest(paths.destination.modules + listInterface[j] + paths.destination.moduleBin));
    gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listModules[i] + '.*')
      .pipe(gulp.dest(paths.destination.moduleRun));
  }

  for (var j = 0; j < listExtensions.length; j++) {
    console.log(paths.source.extensions + listExtensions[j] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listExtensions[j] + '.dll');
    console.log(paths.destination.extension);
    gulp.src(paths.source.extensions + listExtensions[j] + paths.source.moduleBin + 'Debug/netcoreapp*/' + listExtensions[j] + '.dll')
      .pipe(flatten())
      .pipe(gulp.dest(paths.destination.extension));
  }
});
