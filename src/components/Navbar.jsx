import React from 'react'
import './Navbar.css';
import { Link,useLocation } from 'react-router-dom';
import { FaHome, FaHistory } from 'react-icons/fa';

const Navbar = () => {
    const location = useLocation();

    const homeClass = location.pathname === '/' ? 'nav-link active' : 'nav-link';
    const historyClass = location.pathname === '/history' ? 'nav-link active' : 'nav-link';


  return (
      <nav className='navbar'>
            <div  className='logo'>
              <img src="/src/assets/Logo.svg" />
            </div>

{/* ORTA: Home ve History Butonları */}
          <div className='navbar-center'>
              <Link to="/" className={homeClass}>  <FaHome />Home </Link>
              <Link to='/history' className={historyClass}><FaHistory />History</Link>
         </div>       

          <div className='navbar-right'> </div>
      </nav >
  )
}

export default Navbar