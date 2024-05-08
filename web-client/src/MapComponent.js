import React, { useCallback, useEffect, useState } from "react";
import { fromLonLat, toLonLat } from "ol/proj";
import { Point } from "ol/geom";
import { RMap, ROSM, RLayerVector, RFeature, ROverlay, RStyle } from "rlayers";
import locationIcon from "./svg/location.svg";
import AddressPanel from "./AddressPanel";

const coords = {
  origin: [73.3951, 54.9712],
  monument: [73.37006, 54.98430],
};

const MapComponent = () => {
  const [loc, setLoc] = useState(coords.origin);
  const [showAddressPanel, setShowAddressPanel] = useState(false);

  const updateCoordinatesInHTML = (coords) => {
    const latDisplayElement = document.querySelector(".display-lat");
    const lonDisplayElement = document.querySelector(".display-lon");

    latDisplayElement.textContent = coords[1].toFixed(3);
    lonDisplayElement.textContent = coords[0].toFixed(3);
  };

  const handleLatInput = (event) => {
    const lat = parseFloat(event.target.value);
    if (!isNaN(lat)) {
      setLoc([loc[0], lat]);
    } else {
      event.target.value = "";
    }
  };

  const handleLonInput = (event) => {
    const lon = parseFloat(event.target.value);
    if (!isNaN(lon)) {
      setLoc([lon, loc[1]]);
    } else {
      event.target.value = "";
    }
  };

  const handleFindClick = () => {
    const latElement = document.querySelector(".lat");
    const lonElement = document.querySelector(".lon");

    const lat = parseFloat(latElement.value);
    const lon = parseFloat(lonElement.value);

    if (!isNaN(lat) && !isNaN(lon)) {
      setLoc([lon, lat]);
      updateCoordinatesInHTML([lon, lat]);
    }
  };

  useEffect(() => {
    const findButton = document.getElementById("findButton");
    if (findButton) {
      findButton.addEventListener("click", handleFindClick);
    }

    return () => {
      if (findButton) {
        findButton.removeEventListener("click", handleFindClick);
      }
    };
  }, []);

  const toggleAddressPanel = () => {
    setShowAddressPanel(!showAddressPanel);
  };

  return (
    <React.Fragment>
      <RMap
        className="example-map"
        width={"100%"}
        height={"60vh"}
        initial={{ center: fromLonLat(coords.origin), zoom: 16 }}
      >
        <ROSM />
        <RLayerVector>
          <RFeature
            geometry={new Point(fromLonLat(loc))}
            onPointerDrag={useCallback((e) => {
              const coords = e.map.getCoordinateFromPixel(e.pixel);
              e.preventDefault();
              e.target.setGeometry(new Point(coords));
              setLoc(toLonLat(coords));
              updateCoordinatesInHTML(toLonLat(coords));
              return false;
            }, [])}
            onPointerEnter={useCallback(
              (e) => (e.map.getTargetElement().style.cursor = "move") && undefined,
              []
            )}
            onPointerLeave={useCallback(
              (e) => (e.map.getTargetElement().style.cursor = "initial") && undefined,
              []
            )}
          >
            <RStyle.RStyle>
              <RStyle.RIcon src={locationIcon} anchor={[0.5, 1.0]} className="my-svg" />
            </RStyle.RStyle>
            <ROverlay className="example-overlay">Move me</ROverlay>
          </RFeature>
        </RLayerVector>
      </RMap>
      <div className="coord-display">
        <p>Координаты: <span className="display-lat"></span> : <span className="display-lon"></span></p>
      </div>
      <button className="btn btn-primary m-2" onClick={toggleAddressPanel}>
        {showAddressPanel ? "Скрыть адреса" : "Показать адреса"}
      </button>
      {showAddressPanel && <AddressPanel />}
    </React.Fragment>
  );
};

export default MapComponent;
