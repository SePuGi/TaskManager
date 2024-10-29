import './App.css'
import { Route, Routes } from 'react-router-dom'
import Navi from './Componenets/Navi'
import Login from './Componenets/Login'
import ContextLogin from './Controllers/ContextLogin'
import Home from './Componenets/Home'
import { useState } from 'react'
import TaskAdd from './Componenets/TaskAdd'




function App() {
  const [logged, setLogged] = useState(false);
  const [user, setUser] = useState({id:0,nombre:""});

  const canviaLog = (val) => setLogged(val);
  const canviaUser = (val) => setUser(val);
  const contextValues = {
      logged,
      canviaLog,
      user,
      canviaUser
  }
  return (
    <>
      <ContextLogin.Provider value={contextValues}>
        <Navi />
        <Routes>
          <Route path='/' element={<Login />}></Route>
          <Route path='/home' element={<Home />}></Route>
          <Route path='/taskAdd' element={<TaskAdd />}></Route>
          {/*
        <Route path="/edit/:id" element={} /> */}
        </Routes>
      </ContextLogin.Provider>
    </>
  )
}

export default App
