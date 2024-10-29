
import { useContext, useEffect, useState } from "react";
import ContextLogin from "../Controllers/ContextLogin";
import { useNavigate } from "react-router-dom";
import { getUserTask } from "../Controllers/TaskCont";

function Home() {
    const { logged, user } = useContext(ContextLogin);
    const [tareas, setTareas] = useState([]);
    const navigate = useNavigate();
    useEffect(() => {
        if (!logged) {
            navigate("/");
        }
        getUserTask(user.id)
            .then(x => setTareas(x))
    }, [])

    function readyMonthDisplay() {
        const today = new Date();
        const dayToIterate = new Date(today.getFullYear(), today.getMonth(), 1);
        dayToIterate.setDate(dayToIterate.getDate() - dayToIterate.getDay() + 1);
        const days = [];
        const diasSemana = ["Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"]
        for (let i = 0; i < 7; i++) {
            days.push(
                <div className="task2">
                    <h1>{diasSemana[i]}</h1>
                </div>);

        }
        for (let i = 0; i < 35; i++) {
            days.push(
                <div className="task">
                    <h1>{dayToIterate.getDate() + "-" + dayToIterate.getMonth() + "-" + dayToIterate.getFullYear()}</h1>
                    <div>
                        {tareas.map(t => new Date(t.dataInicio) <= dayToIterate && dayToIterate <= new Date(t.dataFin) ? <h3>{t.descripcion}</h3> : <h3></h3>)}
                    </div>
                </div>)
            dayToIterate.setDate(dayToIterate.getDate() + 1);
        }
        return days;

    }

    return (
        <>
            <div className="container">
                {readyMonthDisplay()}
            </div>
            <button onClick={() => navigate("/taskAdd")}>Create Task</button>
        </>
    )
}

export default Home;