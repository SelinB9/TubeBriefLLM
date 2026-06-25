import './App.css'
import Navbar from './components/Navbar'
import Home from './pages/Home';
import History from './pages/History';
import { Routes,Route } from 'react-router-dom'

function App() {

  return (
    <>
      <Navbar /> {/*navbarın her zaman ekranda sabit durması */}
      
      {/*tıklanan butona göre ya home ya history sayfası yükleniyor */}
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/history" element={<History />} />
      </Routes>
    </>
  );
}

export default App
