const mongoose = require('mongoose');

const productSchema = new mongoose.Schema({
    _storeId: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'stores',
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

const Products = mongoose.model('products', productSchema);

module.exports = { Products };
