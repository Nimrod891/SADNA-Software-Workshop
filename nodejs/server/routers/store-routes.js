const { Router } = require('express');
const storesController = require('../controllers/stores-controller');
const router = Router();
const { requireAuth } = require('../middleware/auth-middleware');

router.get('/', storesController.getAllStores);
router.post('/', requireAuth, storesController.createStore);
router.delete('/:id', requireAuth, storesController.deleteStore);
// router.get('/:id', requireAuth, storesController.getProjectsByUser);
// router.put('/invest', requireAuth, storesController.investInProject);
// router.put('/', requireAuth, storesController.editProject);

module.exports = router;
