/// <binding BeforeBuild='clean' AfterBuild='min' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less"),
    flatten = require("gulp-flatten"),
    mainBowerFiles = require("main-bower-files"),
    filter = require("gulp-filter");

var paths = {
    webroot: "./"
};

paths.bower = paths.webroot + "bower_components";

paths.jsSource = paths.webroot + "Js/src";
paths.cssSource = paths.webroot + "Css/src/css";
paths.lessSource = paths.webroot + "Css/src/less";

paths.jsDest = paths.webroot + "Js";
paths.cssDest = paths.webroot + "Css";
paths.fontsDest = paths.webroot + 'Assets/Fonts';
paths.imagesDest = paths.webroot + 'Assets/Images';

paths.tmpDest = paths.webroot + "Css/tmp";
paths.cssTempDest = paths.tmpDest + "/css";
paths.lessTempDest = paths.tmpDest + "/less";

gulp.task('build', ['copy-fonts', 'minify-js', 'minify-css'], function (cb) {
    rimraf(paths.tmpDest, cb);
});

gulp.task('minify-js', ['clean-js'], function () {
    return gulp.src(mainBowerFiles('**/*.js').concat(paths.jsSource + '/**/*.js'))
        .pipe(filter('**/*.js'))
        .pipe(concat('main.js'))
        .pipe(uglify())
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task('minify-css', ['compile-less'], function (cb) {
    return gulp.src(mainBowerFiles('**/*.css').concat([paths.cssSource + '/**/*.css', paths.cssTempDest + '/**/*.css']))
        .pipe(filter('**/*.css'))
        .pipe(concat('main.css'))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.cssDest));
});

gulp.task('compile-less', ['move-custom-less-tmp'], function () {
    return gulp.src([paths.lessTempDest + "/**/build.less", paths.lessSource + '/*.less'])
        .pipe(less())
        .pipe(gulp.dest(paths.cssTempDest));
});

gulp.task('move-custom-less-tmp', ['copy-less-to-tmp'], function () {
    return gulp.src(paths.lessSource + "/**/*.less")
        .pipe(gulp.dest(paths.lessTempDest));
});

gulp.task('copy-less-to-tmp', ['clean-css'], function () {
    return gulp.src(mainBowerFiles({
        filter: '**/*.less',
        overrides: {
            bootstrap: {
                main: [
                    './less/**/*.less'
                ]
            }
        }
    }), { base: paths.bower})
    .pipe(gulp.dest(paths.lessTempDest));
});

gulp.task('copy-fonts', function () {
    return gulp.src(paths.bower + '/**/*.{ttf,woff,woff2,eot,svg}')
        .pipe(flatten())
        .pipe(gulp.dest(paths.fontsDest));
});

gulp.task('copy-images', function () {
    return gulp.src(paths.bower + '/**/*.{png,gif,jpeg,jpg}')
        .pipe(flatten())
        .pipe(gulp.dest(paths.imagesDest));
});

gulp.task("clean-js", function (cb) {
    rimraf(paths.jsDest + '/*.js', cb);
});

gulp.task("clean-css", ["clean-css-generated", "clean-css-tmp"]);

gulp.task("clean-css-generated", function (cb) {
    rimraf(paths.cssDest + '/*.css', cb);
});

gulp.task("clean-css-tmp", function (cb) {
    rimraf(paths.tmpDest, cb);
});