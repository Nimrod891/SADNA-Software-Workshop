const mongoose = require('mongoose');

const productSchema = new mongoose.Schema({
    id: {
        type: mongoose.Schema.Types.ObjectId,
        index: true,
        required: true,
        auto: true,
    },
    name: {
        type: String,
        required: [true, 'Product name is missing'],
    },
    price: {
        type: Number,
        required: [true, 'Product price is missing'],
    },
    imageUrl: {
        type: Array,
        default: [],
    },
    description: {
        type: String,
        default: '',
    },
    inventory: {
        type: Number,
        default: 0,
    },
});

const storeSchema = new mongoose.Schema({
    name: {
        type: String,
        required: [true, 'Store name is missing'],
        unique: true,
    },
    founder: {
        type: String, // username
        required: [true, 'Store founder is missing'],
    },
    products: [productSchema],
});

const Stores = mongoose.model('stores', storeSchema);

module.exports = { Stores };
