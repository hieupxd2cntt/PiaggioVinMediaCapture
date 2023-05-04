import 'package:VinMediaCapture/login/Toast.dart';
import 'package:VinMediaCapture/objectmodel/DocTypeItemAttr.dart';
import 'package:VinMediaCapture/objectmodel/DocTypeItems.dart';
import 'package:VinMediaCapture/objectmodel/model.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class DocTypeItemAddModel {
  DocTypeItems? docTypeItems;
  Model? model;
  DocTypeItemAttr? docTypeItemAttr;
  DocTypeItemAddModel(this.docTypeItems, this.model, this.docTypeItemAttr);
  String prodocValue = "";
  DocTypeItemAddModel.fromJson(Map<String, dynamic> json) {
    docTypeItems = json["DocTypeItems"] == null
        ? null
        : DocTypeItems.fromJson(json["DocTypeItems"]);
    model = json["Model"] == null ? null : Model.fromJson(json["Model"]);
    docTypeItemAttr = json["DocTypeItemAttr"] == null
        ? null
        : DocTypeItemAttr.fromJson(json["DocTypeItemAttr"]);
    prodocValue = json["ProdocValue"] == null ? "" : json["ProdocValue"];
  }
  Map<String, dynamic> toJson() {
    return {
      'docTypeItems': docTypeItems!.toJson(),
      'model': model!.toJson(),
      'docTypeItemAttr': docTypeItemAttr!.toJson(),
      'prodocValue': prodocValue
    };
  }
}
