import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class MobileResult {
  int resultCode;
  String message;
  MobileResult(
    this.resultCode,
    this.message,
  );

  factory MobileResult.fromJson(Map<String, dynamic> json) {
    return new MobileResult(json['resultCode'], json['message']);
  }

  Map<String, dynamic> toJson() {
    return {'resultCode': resultCode, 'message': message};
  }
}
