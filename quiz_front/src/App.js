import Questions from "./components/Questions";
import './components/start.css'
import { useState } from 'react'
import Category from "./components/Category";
import React, { useEffect } from 'react';



function App() {
  const [hidden, setHidden] = useState(true)
  const [getQuestions, setGetQuestions] = useState(false)
  const [getCategory, setCategory] = useState('')

  const goToPath = (child) => {
    setGetQuestions(false)
  }

  const fetchCategory = (child) => {
    
    setGetQuestions(true)
    setCategory(child.target.id)
    console.log("Get category....");       
    console.log(child.target.id);
    console.log(child)

   }

  return (
    <>
      {getQuestions?<Questions back={goToPath} category={getCategory}></Questions>:<Category getCategory={fetchCategory}></Category>}

    </>
  )
}


export default App;
