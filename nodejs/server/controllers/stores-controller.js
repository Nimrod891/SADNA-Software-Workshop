const { Stores } = require('../models/stores');
const { Users } = require('../models/users');

async function getAllStores(req, res) {
    console.log('Getting all stores');
    try {
        const stores = await Stores.find();
        await res.status(200).json(stores);
    } catch (err) {
        await res.status(500).json({
            message: err.message,
        });
    }
}

// async function getProjectsByUser(req, res) {
//     try {
//         if (res.locals.token.id !== req.params.id) {
//             throw new Error('Unauthorized');
//         }
//         const [user] = await Users.find({
//             _id: req.params.id,
//         });
//         const projects = await Stores.find();
//         if (user.type === 'admin') {
//             await res.status(200).json(projects);
//             return;
//         }

//         const filtered = projects.filter((p) => p.owner === user.user);
//         await res.status(200).json(projects.length ? filtered : []);
//     } catch (err) {
//         await res.status(500).json({
//             message: err.message,
//         });
//     }
// }

async function deleteStore(req, res) {
    try {
        await Stores.deleteOne({ _id: req.params.id });
        await res.status(200).json({ message: 'deleted successfully' });
    } catch (err) {
        await res.status(500).json({
            message: err.message,
        });
    }
}

// async function investInProject(req, res) {
//     try {
//         const { projectId, amount } = req.body;
//         const [project] = await Stores.find({
//             _id: projectId,
//         });
//         const newRaised = project.raised + amount;
//         project.raised = newRaised;
//         project.save();
//         await res.status(200).json({
//             newRaised,
//         });
//     } catch (err) {
//         await res.status(500).json({
//             message: err.message,
//         });
//     }
// }

async function createStore(req, res) {
    try {
        const newStore = await Stores.create(req.body);
        await res.status(201).json(newStore);
    } catch (err) {
        await res.status(500).json({
            message: err.message,
        });
    }
}

// async function editStore(req, res) {
//     try {
//         const { projectId: storeId } = req.body;
//         const [store] = await Stores.find({
//             _id: storeId,
//         });
//         Object.assign(store, req.body);
//         store.save();
//         await res.status(200).json(store);
//     } catch (err) {
//         await res.status(500).json({
//             message: err.message,
//         });
//     }
// }

module.exports = {
    getAllStores,
    createStore,
    deleteStore,
    // investInProject,
    // editStore,
    // getProjectsByUser,
};
