import React from 'react'
import { useState, useEffect, useRef } from 'react'
import { Button, Container } from '@mui/material';



const Questions = (props) => {
    const effectRan = useRef(false);

    const [questions, setQuestions] = useState([{
        "correctAnswer": ""
    },
    {
        "correctAnswer": ""
    }])
    const [question, setQuestion] = useState("")
    const [state, setState] = useState();


    useEffect(() => {
        if (effectRan.current === false) {
            const getTasks = async () => {
                const questionsFromServer = await fetchQuestions();
                setQuestions(questionsFromServer);
                setQuestion(questionsFromServer[0].question)
            }

            getTasks();
            console.log(questions);
            console.log('Getting questions....');
            effectRan.current = true;

        }

    }, [])

    // Fetch Questions
    const fetchQuestions = async () => {
        const res = await fetch(`https://localhost:7112/api/question?difficulty=hard&category=${props.category}&count=10`)
        const data = await res.json()
        return data;
    }

    console.log("HELLO HELLO HELLO!!!");

    return (
        <div hidden={props.hidden}>
            <Button onClick={() => console.log(question)}>Click here</Button>

            <Container className='categoryGroup' maxWidth="md">
                <h1>{question}</h1>
                <div className='question' >
                    {questions.map(q => {
                        return <div className='btn1'> <Button size='large' color='success' variant='contained' key={q.question}>{questions[0].correctAnswer}</Button> </div>
                    })}
                    <h1>{questions[0].correctAnswer}</h1>

                    <Button onClick={props.back}>Go back</Button>
                </div>

            </Container>
        </div>
    )
}

export default Questions

// https://localhost:7112/api/question?difficulty=medium&category=music&count=5
// https://jsonplaceholder.typicode.com/posts
// https://localhost:7112/api/question/db/category
// https://localhost:7112/api/question/db