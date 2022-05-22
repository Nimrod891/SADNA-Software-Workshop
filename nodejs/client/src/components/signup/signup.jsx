import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import classes from './signup.module.css';
import Button from '@mui/material/Button';
import { fetcher } from '../../helpers/fetcher';
import * as React from 'react';

const SignUp = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const loginSubmit = async (e) => {
        navigate('/login');
    };

    const handleCreate = async (e) => {
        e.preventDefault();
        const result = await fetcher(`/auth/signup`, 'POST', {
            username,
            password,
        });
        console.log('result', result);
        if (result.status !== 500) {
            alert('Added successfully');
        } else {
            alert('Error: ' + result.message);
        }
    };

    return (
        <div className={classes.root}>
            <form className={classes.form}>
                <div className={classes.title}>SIGN UP</div>
                <TextField
                    label='Username'
                    InputProps={{
                        name: 'username',
                        id: 'username',
                        value: username,
                        autoComplete: 'on',
                        onChange: (e) => setUsername(e.target.value),
                    }}
                />
                <br />
                <TextField
                    label='Password'
                    InputProps={{
                        name: 'password',
                        id: 'password',
                        type: 'password',
                        value: password,
                        autoComplete: 'on',
                        onChange: (e) => setPassword(e.target.value),
                    }}
                />
                <br />

                <Button
                    className={classes.button}
                    type='submit'
                    variant='contained'
                    onClick={handleCreate}
                >
                    Sign Up
                </Button>
                <br />
                <div>
                    <div className={classes.text}>
                        Do you already have an account?
                    </div>
                    <Button
                        className={classes.button}
                        type='submit'
                        variant='contained'
                        onClick={loginSubmit}
                    >
                        Login
                    </Button>
                </div>
                <br />
            </form>
        </div>
    );
};

export default SignUp;
