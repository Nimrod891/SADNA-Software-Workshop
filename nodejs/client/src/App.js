import Home from './components/home/home';
import { Cart } from './components/cart/cart';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import classes from './app.module.css';
import Login from './components/login/login';
import TopBar from './components/top-bar/top-bar';
// import { CreateProject } from './components/create-project/create-project';
// import { EditProject } from './components/edit-project/edit-project';

import SignUp from './components/signup/signup';

function App() {
    return (
        <div className={classes.root}>
            <BrowserRouter>
                <TopBar />
                <Routes>
                    <Route path='/' exact element={<Home />} />
                    <Route path='/cart' exact element={<Cart />} />
                    <Route path='/login' element={<Login />} />
                    {/* <Route path='/create-project' element={<CreateProject />} /> */}
                    {/* <Route path='/edit-project' element={<EditProject />} /> */}
                    <Route path='/signup' element={<SignUp />} />
                </Routes>
            </BrowserRouter>
        </div>
    );
}

export default App;
