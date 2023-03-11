import 'package:dio/dio.dart';
import 'package:json_annotation/json_annotation.dart';

import 'doctypeguide.dart';

@JsonSerializable()
class MobileDoctypeGuideInsertModel {
  DocTypeGuide? docTypeGuide;
  String? currentSession;
  MobileDoctypeGuideInsertModel(this.docTypeGuide, this.currentSession);

  MobileDoctypeGuideInsertModel.fromJson(Map<String, dynamic> json) {
    docTypeGuide = json["docTypeGuide"] == null
        ? null
        : DocTypeGuide.fromJson(json["docTypeGuide"]);
    currentSession = json["CurrentSession"];
  }
  Map<String, dynamic> toJson() {
    return {
      'docTypeGuide': docTypeGuide!.toJson(),
      'currentSession': currentSession
    };
  }
}
