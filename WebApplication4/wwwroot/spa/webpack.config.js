const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: './src/index.tsx',
    mode: 'development',
    devtool: 'inline-source-map',
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.svg/,
                use: {
                    loader: "svg-url-loader",
                    options: {
                        // make all svg images to work in IE
                        iesafe: true,
                    },
                },
            },
            {
                test: /\.css$/i,
                use: ["style-loader", "css-loader",],
            },
        ],
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: 'public/index.html',
        }),
    ],
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
    },
    output: {
        filename: 'bundle.js',
        publicPath: './',
        path: path.resolve(__dirname, 'dist'),
    },
};