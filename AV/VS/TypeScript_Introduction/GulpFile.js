/// <binding BeforeBuild='clean' AfterBuild='default' />
var gulp = require('gulp');
var del = require('del');

// Define file paths.
var paths = {
    scripts: [
        'scripts/**/*.js',
        'scripts/**/*.ts',
        'scripts/**/*.map'
    ],
}

// Clean up previous build.
gulp.task('clean', function () {
    return del(['wwwroot/scripts/**/*']);
});


gulp.task('default', function () {
    gulp.src(paths.scripts).pipe(gulp.dest('wwwroot/scripts'))
});