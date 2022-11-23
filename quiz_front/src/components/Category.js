import React from 'react'
import './category.css'
import { Button, Paper, Container, Box } from '@mui/material';

const Category = (props) => {
    return (
        <div>
            <Container className='categoryGroup' maxWidth="md">
                <h1>Choose a category</h1>

                <div className='category'>
                    <div onClick={props.getCategory} id='computer'>Computer</div>
                </div>
                <div className='category'>
                    <div onClick={props.getCategory} id="movie">Movie</div>
                </div>
                <div className='category'>
                    <div onClick={props.getCategory} id='book'>Book</div>
                </div>
                <div className='category'>
                    <div onClick={props.getCategory} id='history'>History</div>
                </div>
                <div className='category'>
                    <div onClick={props.getCategory} id='videoGame'>Video Game</div>
                </div>
            </Container>
        </div>
    )
}
export default Category