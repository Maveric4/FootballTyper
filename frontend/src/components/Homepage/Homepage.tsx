import React from 'react';
import { Route, Router } from 'react-router-dom';
import './Homepage.scss'
// Components 
import Layout from '../Layout/Layout';
import CountryDict from '../YourBets/MyBets/CountryDict';
import { Team } from '../../App';
import { CircleFlag } from 'react-circle-flags';
// import Carousel  from 'react-bootstrap/Carousel';
import photo4 from './photo4.png';
import photo3 from './photo3.jpg';
import Button from 'react-bootstrap/Button';

import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";
import StadiumCard from './StadiumCard';
export interface HomepageProps{
  allTeams: Team[] | null
}

const responsive = {
  desktop: {
    breakpoint: { max: 3000, min: 1024 },
    items: 3,
    slidesToSlide: 3 // optional, default to 1.
  },
  tablet: {
    breakpoint: { max: 1024, min: 464 },
    items: 2,
    slidesToSlide: 2 // optional, default to 1.
  },
  mobile: {
    breakpoint: { max: 464, min: 0 },
    items: 1,
    slidesToSlide: 1 // optional, default to 1.
  }
};

const Homepage: React.FC<HomepageProps> = ({allTeams}, props) => {
  
  return (
    <div className='homepage-main'>
        <div className='flags'>
          {
            allTeams?.map(({name}, index) => (
              <CircleFlag countryCode={CountryDict.get(name) as string} key={index} className='flag'/>
            ))
          }
        </div>
        <div className='content-body'>
          <span className='welcome-text'>Welcome in Qatar 2022 Typer</span>
        </div>

        <div className='login-info'>

        </div>
        <div style={{gridColumn: '3/10', gridRow: '4/11'}}>


        <h1> Stadiums </h1>
          <Carousel
                swipeable={false}
                draggable={false}
                showDots={true}
                responsive={responsive}
                ssr={true} // means to render carousel on server-side.
                infinite={true}
                autoPlay={ false }
                autoPlaySpeed={1000}
                keyBoardControl={true}
                customTransition="all .5"
                transitionDuration={500}
                containerClass="carousel-container"
                removeArrowOnDeviceType={["tablet", "mobile"]}
                // deviceType={props.deviceType}
                dotListClass="custom-dot-list-style"
                itemClass="carousel-item-padding-40-px"
              >
                  {/* {[photo4, photo3, photo4, photo3].map((photo, index) => (
                  <div style={{width: '20vw'}}>
                    <img src={photo} alt={index.toString()} style={{width: 'inherit', height: '33vh', minWidth: '150px', minHeight: '150px'}}/>
                    <Button>
                      Button
                    </Button>
                  </div>
                ))} */}
                {[photo4, photo3, photo4, photo3].map((photo, index) => (
                  <StadiumCard photo={photo} index={index} key={index} />
                ))}

          </Carousel>
        </div>
    </div>
  )
}

export default Homepage;