import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HistoryService } from '../api/history.service';
import { PicturesService } from '../api/pictures.service';
import { LoadingController } from '@ionic/angular';
import { HistoryRecord } from '../models/favourite-pictures.model';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {

  pictureDetail: HistoryService;
  moreFromBaseWord: Boolean = false;
  morePics: String[]
  loadingDialog: any
  constructor(public activatedRouter: ActivatedRoute, public loadingController: LoadingController, private historyService: HistoryService, private picturesService: PicturesService) { }


  ionViewWillEnter() {
    if (this.historyService.getPictureDetail() != undefined) {
      let obj = this.historyService.getPictureDetail();
      this.pictureDetail = obj.selectedPicture;
      console.log(obj.selectedPicture.id);
    }
  }

  public showMorePick(picture) {

    if (picture !== undefined && picture.length > 0) {
      console.log(picture);
      this.presentLoading();

      this.picturesService.getPictures(picture).subscribe((data) => {
        console.log(data);
        this.morePics = data.photos;


        this.loadingDialog.dismiss();
        this.moreFromBaseWord = true;
        console.log(data.photos);
      });
    }
    else {
      console.log("input is empty");
    }
  }

  public btnSaveFavouriteClicked(picture): void {
    let record = new HistoryRecord(picture);
    this.historyService.saveRecord(record);
  }

  async presentLoading() {
    this.loadingDialog = await this.loadingController.create(
      {
        message: 'Taking photos ...',
      });
    await this.loadingDialog.present();
  }

}
