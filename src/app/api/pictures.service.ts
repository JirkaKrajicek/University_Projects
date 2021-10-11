import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': '563492ad6f91700001000001417c25bc7a354ad595ec38678fdafe4d'
  })
}

@Injectable({
  providedIn: 'root'
})
export class PicturesService {

  constructor(private http: HttpClient) { }

  resultAmount = 10;

  public getPictures(text: String) {
    //api key = 563492ad6f91700001000001417c25bc7a354ad595ec38678fdafe4d
    const url = "https://api.pexels.com/v1/search?query=" + text + "&per_page=" + this.resultAmount;
    return this.http.get<any>(url, httpOptions);
  }

  public getPictureById(id) {
    const url = "https://api.pexels.com/v1/photos/:" + id;
    return this.http.get<any>(url, httpOptions);
  }
}


