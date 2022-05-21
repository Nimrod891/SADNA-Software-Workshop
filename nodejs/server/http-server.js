const express = require('express');
const mongoose = require('mongoose');
const config = require('config');
const cors = require('cors');
const cookieParser = require('cookie-parser');
const authRoutes = require('./routers/login-router');
const storesRoutes = require('./routers/store-routes');
const cartRoutes = require('./routers/cart-routes');

const mongo = config.get('mongo');
const app = express();

app.use(cors());
app.use(express.json());
app.use(cookieParser());

app.use('/auth', authRoutes);
app.use('/stores', storesRoutes);
app.use('/cart', cartRoutes);

(async function () {
    await mongoose.connect(mongo.url, {
        useNewUrlParser: true,
        useUnifiedTopology: true,
    });
    console.log('Connected to MongoDB');
})().catch((err) => console.log(err));

module.exports = app;
