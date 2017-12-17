"use strict";

module.exports = {
    entry: "./wwwroot/js/Tasks/TaskInit.js",
    output: {
        filename: "./wwwroot/js/dist/bundle.js"
    },
    resolve: {
        alias: {
            jquery: "./wwwroot/lib/jquery/dist/jquery.min.js"
        }
    }
    
};