import React from 'react'
import { Button, Container } from '@mui/material';


const Category = () => {
    return (
        <div className='background'>
            <Container className='categoryGroup' maxWidth="sm">
                <h1>Choose a category</h1>
                <div className='btnGroup'>
                    <div className='btn1'>
                        <Button color='info' variant='contained' href="#contained-buttons">Movie</Button>
                    </div>
                    <div className='btn1'>
                        <Button color='warning' variant='contained' href="#contained-buttons2">Computer</Button>
                    </div>
                    <div className='btn1'>
                        <Button color='error' variant='contained' href="#contained-buttons3">History</Button>
                    </div>
                    <div className='btn1'>
                        <Button color='success' variant='contained' href="#contained-buttons4">Literature</Button>
                    </div>
                </div>
            </Container>
        </div>
    )
}

export default Category