// src/setupProxy.js
const { createProxyMiddleware } = require('http-proxy-middleware');

const middleware = createProxyMiddleware({ target: 'http://localhost:3000' });

module.exports = function (app) {
    app.use('/auth', middleware);
    app.use('/stores', middleware);
    app.use('/cart', middleware);
};
