import React from 'react'
import { useState, useEffect } from 'react'
import { Button, Paper, Container, Box } from '@mui/material';


class Rectangle {
  constructor(height, width) {
    this.height = height;
    this.width = width;
  }
}


const Questions = (props) => {
  const [questions, setQuestions] = useState([{
    "correctAnswer":""
  },
  {
    "correctAnswer":""
  }])
  const [question, setQuestion] = useState("")
  const [state, setState] = useState();


  useEffect(() => {
    const getTasks = async () => {
      const questionsFromServer = await fetchQuestions();
      setQuestions(questionsFromServer);
      setQuestion(questionsFromServer[0].question)
    }

    getTasks();
    console.log(questions);
    console.log('Getting questions....');

  }, [])

  // Fetch Questions
  const fetchQuestions = async () => {
    const res = await fetch(`https://localhost:7112/api/question?difficulty=medium&category=${props.category}&count=5`)
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
          return  <div className='btn1'> <Button size='large' color='success' variant='contained' key={q.question}>{questions[0].correctAnswer}</Button> </div>
        })}
        <h1>{questions[0].correctAnswer}</h1>

       <Button onClick={props.back}>Go back</Button>

    

          {/* <div className='btn1'>
            <Button size='large' color='success' variant='contained'>ABCDEEF</Button>
          </div>
          <div className='btn1'>
            <Button size='large' color='success' variant='contained'>BBBBBBB</Button>
          </div>
          <div className='btn1'>
            <Button size='large' color='success' variant='contained'>Cddd</Button>
          </div>
          <div className='btn1'>
            <Button size='large' color='success' variant='contained'>Dgg</Button>
          </div> */}
        </div>
        {/* {questions.map(q => {
          return <h1 key={q.question}>{questions[0].correctAnswer}</h1>
        })}
        {questions.map(q => {
          return <h1 key={q.question}>{questions[0].incorrectAnswers}</h1>
        })} */}

      </Container>
    </div>
  )
}

export default Questions

// https://localhost:7112/api/question?difficulty=medium&category=music&count=5
// https://jsonplaceholder.typicode.com/posts
// https://localhost:7112/api/question/db/category
// https://localhost:7112/api/question/db