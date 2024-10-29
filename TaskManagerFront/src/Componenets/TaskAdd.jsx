import { useState } from "react";
import { addTask } from "../Controllers/TaskCont";
import { useNavigate } from "react-router-dom";

function TaskAdd() {
    const [userId, setUserid] = useState(0);
    const [desc, setDesc] = useState("");
    const [finit, setFinit] = useState(0);
    const [fend, setFend] = useState(0);
    const navigate = useNavigate();

    function enviarForm(e) {
        e.preventDefault();
        addTask({ "idUsuario": userId*1, "descripcion": desc, "dataInicio": finit, "dataFin": fend })
            .then(navigate("/home"))
    }

    return (
        <>
            <form onSubmit={enviarForm}>
                <label htmlFor="userId">Id Usuario:</label>
                <input type="number" id="userId" value={userId} onChange={(e) => setUserid(e.target.value)} required />

                <label htmlFor="desc">Descripcion</label>
                <input type="text" id="desc" value={desc} onChange={(e) => setDesc(e.target.value)} required />

                <label htmlFor="finit">Fecha de inicio</label>
                <input type="date" id="finit" value={finit} onChange={(e) => setFinit(e.target.value)} required />

                <label htmlFor="fend">Fecha de final</label>
                <input type="date" id="fend" value={fend} onChange={(e) => setFend(e.target.value)} required />

                <button type="submit"> Send</button>
            </form>
        </>
    )
}

export default TaskAdd;