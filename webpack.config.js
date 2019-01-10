var path = require("path");
var webpack = require("webpack");

function resolve(filePath) {
    return path.join(__dirname, filePath);
}

var babelOptions = {
    presets: [
        ["@babel/preset-env", {
            "targets": "> 0.25%, not dead",
            "modules": false,
            // This adds polyfills when needed. Requires core-js dependency.
            // See https://babeljs.io/docs/en/babel-preset-env#usebuiltins
            "useBuiltIns": "usage"
        }]
    ]
};


var isProduction = process.argv.indexOf("-p") >= 0;
var port = process.env.SUAVE_FABLE_PORT || "8085";
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
    devtool: "source-map",
    entry: [ resolve("./src/Client/Client.fsproj") ],
    output: {
        path: resolve('./src/Client/public'),
        publicPath: "/",
        filename: "bundle.js"
    },
    resolve: {
        modules: [resolve("./node_modules/")]
    },
    devServer: {
        proxy: {
            '/': {
                target: 'http://localhost:' + port,
                changeOrigin: true
            },
            '/api/*': {
                target: 'http://localhost:' + port,
                changeOrigin: true
            }
        },
        hot: true,
        inline: true,
        contentBase: './src/Client/'
    },
    module: {
        rules: [
            {
                test: /\.fs(x|proj)?$/,
                use: {
                    loader: "fable-loader",
                    options: {
                        babel: babelOptions,
                        define: isProduction ? [] : ["DEBUG"]
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: babelOptions
                }
            }
        ]
    },
    plugins: isProduction ? [] : [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin()
    ],
    mode: isProduction ? 'production' : 'development'
};
