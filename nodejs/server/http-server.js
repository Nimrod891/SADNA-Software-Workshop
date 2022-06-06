const express = require('express');

const cors = require('cors');
const cookieParser = require('cookie-parser');
const authRoutes = require('./routers/login-router');
const storesRoutes = require('./routers/store-routes');
const productsRoutes = require('./routers/products-routes');
const cartRoutes = require('./routers/cart-routes');
const { connectToDb } = require('./dao/connect-to-db');

const app = express();

app.use(cors());
app.use(express.json());
app.use(cookieParser());

app.use('/auth', authRoutes);
app.use('/stores', storesRoutes);
app.use('/products', productsRoutes);
app.use('/cart', cartRoutes);

(async function () {
    await connectToDb();
})();

module.exports = app;
