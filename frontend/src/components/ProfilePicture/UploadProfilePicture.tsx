import React, { useState, useReducer, useRef } from "react";
import ProfilePicture from "./ProfilePicture";
import './UploadProfilePicture.scss';

const UploadProfilePicture = () => {
  const [file, setFile] = useState<any>();
  const [fileName, setFileName] = useState<any>();
  const ref = useRef<HTMLInputElement>(null);
  //   const apiUrl = "http://localhost:44302/";
  const API_URL = process.env.REACT_APP_IS_IT_PRODUCTION_VERSION === 'true' ? process.env.REACT_APP_API_URL_PROD : process.env.REACT_APP_API_URL_LOCAL;

  const user = localStorage.getItem("user")
    ? JSON.parse(localStorage.getItem("user") as string)
    : "";

  const [imgBlobLink, setImgBlobLink] = useState<string>(user.imgLink);
  const sendHttpRequest = async (path: string, requestOptions: any) => {
    await fetch(API_URL + path, requestOptions).then((response) => {
      // console.log("Response: ", response);
      if (response.ok) {
        return response.json();
      }
    });
    // .then((data) => {
    //   console.log(data);
    // });
  };

  const uploadFile = async () => {
    if (validateFileType()) {
      let newImgLink =
        "https://footballtypersa.blob.core.windows.net/imgs/" +
        user.username +
        "__" +
        new Date().toLocaleTimeString() +
        "." +
        getExt(fileName);
      const formData = new FormData();
      formData.append("File", file);
      formData.append("FileName", newImgLink);

      const postRequestOptions = {
        method: "POST",
        body: formData,
      };
      await sendHttpRequest(
        API_URL + "api/File",
        postRequestOptions
      );

      const putRequestOptions = {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("userToken")}`,
        },
        body: JSON.stringify({
          username: user.username,
          imgLink: newImgLink,
        }),
      };
      await sendHttpRequest(
        API_URL + `api/TyperUsers/ImgLink/${user.id}`,
        putRequestOptions
      );

      localStorage.setItem(
        "user",
        JSON.stringify({
          username: user.username,
          email: user.email,
          fullName: user.fullName,
          id: user.id,
          imgLink: newImgLink,
          leagues: user.leagues,
        })
      );
      setImgBlobLink(newImgLink);
    }
  };

  const saveFile = async (e: any) => {
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].name);
  };

  const validateFileType = () => {
    var extFile = getExt(fileName);
    if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
      return true;
    } else {
      alert("Only jpg/jpeg and png files are allowed!");
      return false;
    }
  };

  const getExt = (fileName: string) => {
    var idxDot = fileName.lastIndexOf(".") + 1;
    return fileName.substr(idxDot, fileName.length).toLowerCase();
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <button style={{ marginTop: "1vh" }} onClick={() => ref.current?.click()}>
        Select Profile Picture
      </button>
      <input
        id="filePicker"
        style={{ margin: "1vh", display: "none" }}
        type="file"
        onChange={saveFile}
        accept="image/*"
        ref={ref}
      />

      <input
        className="upload-button"
        type="button"
        value="Upload Profile Picture"
        onClick={async () => await uploadFile()}
      />

    </div>
  );
};

export default UploadProfilePicture;
