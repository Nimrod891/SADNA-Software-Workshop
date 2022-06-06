const { Router } = require('express');
const { ProductsController } = require('../controllers/products-controller');
const router = Router();
const { requireAuth } = require('../middleware/auth-middleware');

const productsController = new ProductsController();

router.post('/', productsController.create);

module.exports = router;
