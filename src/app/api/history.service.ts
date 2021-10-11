import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { HistoryRecord } from '../models/favourite-pictures.model';
import { PicturesService } from '../api/pictures.service';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  historyArray: HistoryRecord[] = [];
  pictureDetail: HistoryRecord;

  constructor(private storage: Storage) {
    storage.get('favourite').then((val) => {
      if (val) {
        this.historyArray = JSON.parse(val);
      }
    });
  }

  public saveRecord(record: HistoryRecord) {
    if (this.historyArray.find(obj => obj.selectedPicture.id == record.selectedPicture.id) == undefined) {
      this.historyArray.unshift(record);
      this.storage.set('favourite', JSON.stringify(this.historyArray));
    }
  }

  public getRecord() {
    return this.historyArray;
  }

  public removeRecord(record: any) {
    this.historyArray = this.historyArray.filter(obj => obj != record);
    this.storage.clear();
    this.storage.set('favourite', JSON.stringify(this.historyArray));
  }

  public passPictureDetail(picture: any) {
    this.pictureDetail = picture;
  }

  public getPictureDetail() {
    return this.pictureDetail;
  }
} 