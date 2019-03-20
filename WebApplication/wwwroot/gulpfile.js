const gulp = require('gulp');
const concat = require('gulp-concat');
const handlebars = require('gulp-handlebars');
const wrap = require('gulp-wrap');
const declare = require('gulp-declare');
const clean_css = require('gulp-clean-css');
const auto_prefixer = require('gulp-autoprefixer');
const browserSync = require('browser-sync').create();
const order = require('gulp-order');
const babel = require('gulp-babel');
const uglify = require('gulp-uglifyjs');

const html = function () {
    return gulp.src('./html/**/*.html')
        .pipe(gulp.dest('dist'))
};

const css = function () {
    return gulp.src('./css/**/*.css')
        .pipe(clean_css({compatibility: 'ie9'}))
        .pipe(auto_prefixer('last 2 version', 'safari 5', 'ie 9'))
        .pipe(concat('style.min.css'))
        .pipe(gulp.dest('./dist/css'))
        .pipe(browserSync.stream());
};

const files_js_order = [
    'js/spa.js',
    'js/features/*.js'
];

const js = function () {
    return gulp.src("./js/**/*.js")
        .pipe(order(files_js_order, {base: './'}))
        .pipe(concat('app.js'))
        .pipe(babel({
            presets: ['@babel/env']
        }))
        .pipe(uglify({compress: true}))
        .pipe(gulp.dest('./dist/js/'))
        .pipe(browserSync.stream());
};

const vendor = function(){
    return gulp.src(['vendor/handlebars-runtime-3/handlebars.js'])
        .pipe(concat('vendor.js'))
        .pipe(gulp.dest('dist/js'));
};

const templates = function(){
    return gulp.src(['templates/**/*.hbs'])
        .pipe(handlebars())
        .pipe(wrap('Handlebars.template(<%= contents %>)'))
        .pipe(declare({
            namespace: 'spa_templates',
            noRedeclare: true,
            processName: function(filePath) {
                return declare.processNameByPath(filePath.replace('client/templates/', ''));
            }
        }))
        .pipe(concat('templates.js'))
        .pipe(gulp.dest('./dist/js/'))
        .pipe(browserSync.stream());
};

gulp.task('serve', function() {
    browserSync.init({
        server: {
            baseDir: "./dist/",
            index: "game.html"
        }
    });

    gulp.watch("./css/**.css", gulp.series(css));
    gulp.watch("./js/**/*.js", gulp.series(js));
    gulp.watch("./templates/**/*.hbs", gulp.series(templates));
    gulp.watch("./dist/**/*.html").on('change', browserSync.reload);
});

exports.build = gulp.parallel(html, css, js, vendor, templates);